using R12.Common;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using R12.Models;

namespace R12.Services
{
    public static class Repository
    {
        #region Util
        internal static bool IsEndpointReachable()
        {
            App.IsEndpointConnected = CrossConnectivity.Current.IsConnected;
            return CrossConnectivity.Current.IsConnected;
        }
        private static async Task OnStartService()
        {
            // Intenta sincronizar
            var local_syncros = App.Database.GetAllSyncroAsync().Result;

            foreach (var syncroItem in local_syncros)
            {
                await EndpointSyncro.TargetDecider(syncroItem);
            }
        }
        internal static async Task Log_CreateAsync(LogMessageModel log)
        {
            log.LogAt = DateTime.Now;
            await App.Database.LogAsync(log);
        }

        internal static async Task Logging_DeleteAll()
        {
            await App.Database.Log_DeleteAll();
        }

        internal static async Task<LogMessageModel> Log_GetAsyn(long id)
        {
            return await App.Database.Log_GetAsync(id);
        }
        internal static async Task<List<LogMessageModel>> Log_GetAllAsync()
        {
            return await App.Database.Log_GetAllAsync();
        }
        internal static async Task<ApiResponse> Log_SendAsync(LogMessageModel log)
        {
            ApiResponse response = new ApiResponse();
            var content = JsonConvert.SerializeObject(log);
            try
            {
                var res = await new EndpointService().PostContentAsync(EndpointRestUri.LOGGING_Create, content);
                if (res == "")
                    response = new ApiResponse { isOk = false, Message = "Falla del envio de log."};
                else
                    response = JsonConvert.DeserializeObject<ApiResponse>(res);
            }
            catch (Exception ex)
            {
                // nada
            }
            return response;
        }
        #endregion
        #region Auth
        internal static async Task<APIResponse> LoginAsync(Andromeda.LoginRequestModel login)
        {
            try
            {
                // Rest
                var token = await new EndpointService().PostContentNoHeaderAsync(EndpointRestUri.LOGIN_Authenticate, JsonConvert.SerializeObject(login));
                // Seccion SQLite
                if (token.Length > 1)
                {
                    await App.Database.UpdateLogin(login);
                    await App.Database.UpdateLoginToken(token);
                    return new APIResponse { IsOk = true, Message = "" };
                }
                else
                {
                    return new APIResponse { IsOk = false, Message = "Usuario o contraseña incorrectos" };
                }


            }
            catch (Exception ex)
            {
                return new APIResponse { IsOk = false, Message = "No se pudo establecer conexión con el servidor: " + ex.Message };
            }
        }
        internal static async Task<bool> IsUserLoggedAsync()
        {
            var userModel = await App.Database.GetUserModelLogged();
            if (userModel.Username != null)
            {
                var res = LoginAsync(new Andromeda.LoginRequestModel { Username = userModel.Username, Password = userModel.Password });
                return true;
            }
            return false;
        }
        internal static async Task<string> GetUserLogged()
        {
            return await App.Database.GetUserLogged();
        }
        internal static async Task Logout()
        {
            await App.Database.Logout();
        }
        #endregion
        #region Sync/Andromeda
        internal static async Task Andromeda_SyncAsync()
        {
            // borrar base entities y items

            //obtener entities clientes
            var resultEnt = await new EndpointService().GetWithKeyAsync(EndpointRestUri.ENTITY_GetByType, "1");
            var entities = JsonConvert.DeserializeObject<List<EntityModel>>(resultEnt);

            //obtener categories
            var resultCat = await new EndpointService().GetAsync(EndpointRestUri.CATEGORY_GetAll);
            var categories = JsonConvert.DeserializeObject<List<CategoryModel>>(resultCat);

            //obtener items
            var resultIte = await new EndpointService().GetAsync(EndpointRestUri.ITEM_GetAll);
            var items = JsonConvert.DeserializeObject<List<ItemModel>>(resultIte);

            //grabarlos localmente
            await App.Database.BeforeAllSyncroAsync();
            await App.Database.SaveEntitiesAsync(entities);
            await App.Database.SaveItemsAsync(items);
            await App.Database.SaveCategoriesAsync(categories);
        }
        private static async Task<ApiResponse> Andromeda_SendDocumentAsync(EntityModel entity, List<DocumentLineModel> lines)
        {
            ApiResponse response;
            DocumentModel doc = new DocumentModel
            {
                EntityId = entity.EntityId,
                DocumentDate = DateTime.UtcNow,
                DocumentLinesQty = lines.Count,
                DocumentLetter = "P",
                DocumentStatus = 1,
                Detail = lines,
                DocumentType = 1
            };
            var content = JsonConvert.SerializeObject(doc);
            try
            {
                var res = await new EndpointService().PostContentAsync(EndpointRestUri.DOCUMENT_Create, content);
                if (res == "")
                    response = new ApiResponse { isOk = false, Message = "Error al enviar. El pedido se encuentra en el teléfono guardado. Reintente nuevamente."};
                else
                    response = JsonConvert.DeserializeObject<ApiResponse>(res);
            }
            catch (Exception ex)
            {
                var log = new LogMessageModel
                {
                    LogCode = "Andromeda_SendDocumentAsync",
                    LogDescription = ex.Message,
                    LogMessageException = ex.Message
                };
                await Repository.Log_CreateAsync(log);
                response = new ApiResponse { isOk = false, Message = ex.Message };
            }
            return response;
        }
        internal static async Task<ApiResponse> Andromeda_TrySendDocumentAsync(EntityModel entity)
        {
            // Intenta enviar el pedido temporal
            ApiResponse result;
            List<DocumentLineModel> lines = await Temporal_GetLinesAsync();
            try
            {
                result = await Andromeda_SendDocumentAsync(entity, lines);
                if (result.isOk == true)
                {
                    result.Message = "Envío correcto.";
                    var Doc = JsonConvert.SerializeObject(lines);
                    var Del = JsonConvert.DeserializeObject<List<DeliveryLineModel>>(Doc);
                    var savedId = await App.Database.SaveLocalDelivery((int)entity.EntityId, Del, 1);
                    await App.Database.SaveNumberDelivery(savedId, result.NumericMessage);
                }
                else
                {
                    var log = new LogMessageModel
                    {
                        LogCode = "Andromeda_TrySendDocumentAsync",
                        LogDescription = "Pedido: " + JsonConvert.SerializeObject(result)
                    };
                    await Repository.Log_CreateAsync(log);

                    var Doc = JsonConvert.SerializeObject(lines);
                    var Del = JsonConvert.DeserializeObject<List<DeliveryLineModel>>(Doc);
                    await App.Database.SaveLocalDelivery((int)entity.EntityId, Del, 0);
                    result = new ApiResponse { isOk = false, Message = "" };
                }
            }
            catch (Exception ex)
            {
                var log = new LogMessageModel
                {
                    LogCode = "Andromeda_TrySendDocumentAsync",
                    LogDescription = "Pedido: " + JsonConvert.SerializeObject(lines),
                    LogMessageException = ex.Message
                };
                await Repository.Log_CreateAsync(log);

                var Doc = JsonConvert.SerializeObject(lines);
                var Del = JsonConvert.DeserializeObject<List<DeliveryLineModel>>(Doc);
                await App.Database.SaveLocalDelivery((int)entity.EntityId, Del, 0);
                result = new ApiResponse { isOk = false, Message = "PEDIDO NO ENVIADO: Se guardó internamente, ver cola de envío." };
            }
            return result;
        }
        internal static async Task<ApiResponse> Andromeda_TrySendPendingAsync()
        {
            // Intenta enviar los pedidos guardado
            ApiResponse result;
            int andromeda_count = 0;
            int failed_count = 0;
            List<DeliveryModel> deliveries = await Delivery_GetPendingAsync();
            if (deliveries.Count > 0)
            {
                foreach (var delivery in deliveries)
                {
                    if(delivery._DocumentStatus == 0)
                    {
                        try
                        {
                            var entityModel = await App.Database.GetEntityByIdAsync((int)delivery._EntityId);
                            List<DocumentLineModel> lines = JsonConvert.DeserializeObject<List<DocumentLineModel>>(JsonConvert.SerializeObject(delivery.Detail));
                            result = await Andromeda_SendDocumentAsync(entityModel, lines);
                            if (result.isOk == true)
                            {
                                await Delivery_SetStatusAsync((long)delivery._DocumentId, 1);
                                await App.Database.SaveNumberDelivery((long)delivery._DocumentId, result.NumericMessage);
                                andromeda_count++;
                            }
                            else
                            {
                                var log = new LogMessageModel
                                {
                                    LogCode = "Andromeda_TrySendPendingAsync",
                                    LogDescription = JsonConvert.SerializeObject(result),
                                };
                                await Repository.Log_CreateAsync(log);
                                failed_count++;
                            }
                        }
                        catch (Exception ex)
                        {
                            var log = new LogMessageModel
                            {
                                LogCode = "Andromeda_TrySendPendingAsync",
                                LogDescription = "Pedido: " + JsonConvert.SerializeObject(delivery),
                                LogMessageException = ex.Message
                            };
                            await Repository.Log_CreateAsync(log);
                            failed_count++;
                        }
                    }
                }
            }
            return result = new ApiResponse { isOk=true, Message="Enviados: "+andromeda_count.ToString()+ " Sin enviar: "+failed_count.ToString()};
        }
        #endregion
        #region Temporal
        internal static async Task<List<DocumentLineModel>> Temporal_GetLinesAsync()
        {
            return await App.Database.GetTempDelLinesAsync();
        }
        internal static async Task<DocumentLineModel> TemporalGetLineByItemAsync(long id)
        {
            return await App.Database.GetTempDelLineAsync(id);
        }
        internal static async Task<int> Temporal_GetCountLinesAsync()
        {
            var items = await App.Database.GetTempDelLinesAsync();
            return items.Count;
        }
        internal static async Task Temporal_ManageAsync(int qty, ItemModel item)
        {
            if (qty >= 0)
            {
                // obtiene item
                if (item.ItemId > 0)
                {
                    // recupera linea de temporal, ABM
                    var tempDelLine = await App.Database.GetTempDelLineByItemIdAsync(item.ItemId);

                    if (tempDelLine is null)
                    {
                        // alta
                        DocumentLineModel newTempDelLine = new DocumentLineModel
                        {
                            DocumentLineItemDescription = item.ItemDescription,
                            DocumentLineItemId = item.ItemId,
                            DocumentLineQty = qty
                        };
                        await App.Database.AddTempDelLineAsync(newTempDelLine);
                    }
                    else
                    {
                        // update o baja
                        tempDelLine.DocumentLineQty = qty;
                        await App.Database.SaveTempDelLineAsync(tempDelLine);
                    }
                }
            }
        }
        internal static async Task Temporal_DeleteAsync()
        {
            await App.Database.DeleteTempDeliveryAsync();
        }
        #endregion
        #region Delivery
        internal static async Task<int> Delivery_GetCountPendingAsync()
        {
            var deliveriesPending = await App.Database.GetLocalDeliveriesAsync();
            return deliveriesPending.Count;
        }
        internal static async Task<List<DeliveryModel>> Delivery_GetAllAsync()
        {
            List<DeliveryModel> deliveries = await App.Database.GetLocalDeliveriesAsync();
            if (deliveries != null)
            {
                foreach (var delivery in deliveries)
                {
                    var entityModel = await App.Database.GetEntityByIdAsync((int)delivery._EntityId);
                    int qty = 0;
                    foreach (var line in delivery.Detail)
                        qty++;
                    delivery._DocumentLinesQty = qty;
                }
            }
            return deliveries;
        }
        internal static async Task<List<DocumentModel>> Delivery_GetByStatusAsync(int status)
        {
            //obtener documentos en entrega
            try
            {
                var result = await new EndpointService().GetWithKeyAsync(EndpointRestUri.DOCUMENT_GetByStatus, status.ToString());
                return JsonConvert.DeserializeObject<List<DocumentModel>>(result);
            }
            catch 
            {
                return new List<DocumentModel>();
            }
        }
        internal static async Task<DocumentModel> Delivery_GetByIdAsync(long id)
        {
            //obtener documento por id
            try
            {
                var result = await new EndpointService().GetWithKeyAsync(EndpointRestUri.DOCUMENT_GetById, id.ToString());
                return JsonConvert.DeserializeObject<DocumentModel>(result);
            }
            catch
            {
                return new DocumentModel();
            }
        }
        internal static async Task<ApiResponse> Delivery_SetDelivered(DocumentModel document)
        {
            //obtener documentos en entrega
            var result = await new EndpointService().PostContentAsync(EndpointRestUri.DOCUMENT_SetDelivered, JsonConvert.SerializeObject(document));
            return JsonConvert.DeserializeObject<ApiResponse>(result);
        }
        internal static async Task<ApiResponse> Delivery_SetSigned(DocumentModel document)
        {
            //obtener documentos en entrega
            var result = await new EndpointService().PostContentAsync(EndpointRestUri.DOCUMENT_SetSigned, JsonConvert.SerializeObject(document));
            return JsonConvert.DeserializeObject<ApiResponse>(result);
        }
        internal static async Task<List<DeliveryModel>> Delivery_GetPendingAsync()
        {
            List<DeliveryModel> deliveries = await App.Database.GetLocalDeliveriesAsync(true);
            if (deliveries != null)
            {
                foreach(var delivery in deliveries)
                {
                    var entityModel = await App.Database.GetEntityByIdAsync((int)delivery._EntityId);
                    int qty = 0;
                    foreach (var line in delivery.Detail)
                        qty++;
                    delivery._DocumentLinesQty = qty;
                }
            }
            return deliveries;
        }
        internal static async Task<DocumentModel> Delivery_Remote_GetLast()
        {
            try
            {
                var result = await new EndpointService().GetAsync(EndpointRestUri.DOCUMENT_GetLast);
                var lastDoc = JsonConvert.DeserializeObject<DocumentModel>(result);
                lastDoc.EntityModel = await App.Database.GetEntityByIdAsync((int)lastDoc.EntityId);
                return lastDoc;
            }
            catch (Exception)
            {
                return new DocumentModel { EntityModel = new EntityModel { EntityDescription = "Información no disponible."} };
            }
        }
        internal static async Task Delivery_DeleteAll()
        {
            await App.Database.DeleteAllDeliveriesAsync();
        }
        internal static async Task Delivery_DeleteByIdAsync(long id)
        {
            await App.Database.DeleteDeliveryAsync(id);
        }
        internal static async Task Delivery_SetStatusAsync(long id, int status)
        {
            await App.Database.SaveStatusDelivery(id, status);
        }
        #endregion
        #region Entity
        internal static async Task<List<EntityModel>> Entity_GetAllAsync()
        {
            return await App.Database.GetLocalEntitiesAsync();
        }
        internal static async Task<EntityModel> Entity_GetByIdAsync(int id)
        {
            return await App.Database.GetEntityByIdAsync(id);
        }
        #endregion
        #region Item
        public static async Task<ItemModel> Item_GetByIdAsync(int id)
        {
            return await App.Database.GetLocalItemById(id);
        }
        public static async Task<ItemModel> Item_GetByBarcodeAsync(string barcode)
        {
            return await App.Database.GetLocalItemByBarcode(barcode);
        }
        internal static async Task<List<Models.ItemModel>> Item_GetAllAsync()
        {
            return await App.Database.GetLocalItemsAsync();
        }
        internal static async Task<List<Models.ItemModel>> Item_GetByCategoryAsync(int id)
        {
            return await App.Database.GetLocalItemsByCategoryAsync(id);
        }
        #endregion
        #region Category
        internal static async Task<List<CategoryModel>> Category_GetAllAsync()
        {
            return await App.Database.GetLocalCategoriesAsync();
        }
        internal static async Task<CategoryModel> Category_GetById(int id)
        {
            return await App.Database.GetCategoryById(id);
        }
        #endregion        
    }

    internal static class EndpointSyncro
    {
        public static async Task TargetDecider(SyncroModel sync)
        {
            switch (sync.SyncroTargetObject)
            {
                case ObjectTable.Document:
                    break;
                default:
                    await App.Database.DeleteSyncroById(sync);
                    break;
            }
        }
    }

    internal class EndpointService
    {
        private HttpClient _client = new HttpClient { BaseAddress = EndpointRestUri.URL_SERVICIO };

        public async Task<string> PostContentNoHeaderAsync(string requestUri, string content)
        {
            if (!App.IsEndpointConnected)
                throw new Exception("NotReachable");
            var token = App.Database.GetToken().Result;
            _client.Timeout = TimeSpan.FromSeconds(12);
            var body = new StringContent(content, Encoding.UTF8, EndpointMediaType.applicationJson);
            return await _client.PostAsync(requestUri, body).Result.Content.ReadAsStringAsync();
        }

        public async Task<string> PostContentAsync(string requestUri, string content)
        {
            if (!App.IsEndpointConnected)
                throw new Exception("NotReachable");
            var token = App.Database.GetToken().Result;
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            _client.Timeout = TimeSpan.FromSeconds(12);
            var body = new StringContent(content, Encoding.UTF8, EndpointMediaType.applicationJson);
            return await _client.PostAsync(requestUri, body).Result.Content.ReadAsStringAsync();
        }

        public async Task<string> PostContentAsync(string requestUri, string content, int timeout)
        {
            if (!App.IsEndpointConnected)
                throw new Exception("NotReachable");
            var token = App.Database.GetToken().Result;
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            _client.Timeout = TimeSpan.FromSeconds(timeout);
            var body = new StringContent(content, Encoding.UTF8, EndpointMediaType.applicationJson);
            return await _client.PostAsync(requestUri, body).Result.Content.ReadAsStringAsync();
        }

        public async Task<string> PostWithKeyAsync(string requestUri, string key)
        {
            if (!App.IsEndpointConnected)
                throw new Exception("NotReachable");
            var token = App.Database.GetToken().Result;
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var content = new StringContent("", Encoding.UTF8, EndpointMediaType.applicationJson);
            return await _client.PostAsync(requestUri + key, content).Result.Content.ReadAsStringAsync();
        }

        public async Task<string> GetAsync(string requestUri)
        {
            if (!App.IsEndpointConnected)
                throw new Exception("NotReachable");
            var token = App.Database.GetToken().Result;
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            return await _client.GetAsync(requestUri).Result.Content.ReadAsStringAsync();
        }

        public async Task<string> GetWithKeyAsync(string requestUri, string key)
        {
            if (!App.IsEndpointConnected)
                throw new Exception("NotReachable");
            var token = App.Database.GetToken().Result;
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            return await _client.GetAsync(requestUri + key).Result.Content.ReadAsStringAsync();
        }
    }

    internal static class EndpointMediaType
    {
        public static string applicationJson = "application/json";
    }

    internal static class EndpointRestUri
    {
        public static Uri URL_SERVICIO = new Uri("URL DEL SERVICIO ** COMPLETAR", UriKind.Absolute);
        public static string LOGIN_EchoPing = "login/echoping";
        public static string LOGIN_Authenticate = "login/authenticate";

        public static string LOGGING_Create = "logging/create";

        public static string ENTITY_GetByType = "entity/GetByType?type=";
        public static string ENTITY_GetById = "entity/GetById?id=";

        public static string ITEM_GetAll = "item/GetAll";
        public static string ITEM_GetById = "item/GetById?id=";

        public static string DOCUMENT_GetByStatus = "document/GetByStatus?status=";
        public static string DOCUMENT_GetById = "document/GetById?id=";
        public static string DOCUMENT_Create = "document/create";
        public static string DOCUMENT_GetLast = "document/getlast";
        public static string DOCUMENT_SetDelivered = "document/SetDelivered";
        public static string DOCUMENT_SetSigned = "document/SetNotIncome";

        public static string CATEGORY_GetAll = "category/getall";
    }

    

}
