using Newtonsoft.Json;
using R12;
using R12.Models;
using R12.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace R12.Services
{
    public class DataStore : IDataStore
    {

        readonly SQLiteAsyncConnection _database;

        public DataStore(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<LogMessageModel>().Wait();
            _database.CreateTableAsync<UserAuthModel>().Wait();
            _database.CreateTableAsync<R12.Models.EntityModel>().Wait();
            _database.CreateTableAsync<R12.Models.CategoryModel>().Wait();
            _database.CreateTableAsync<R12.Models.ItemModel>().Wait();
            _database.CreateTableAsync<R12.Models.DocumentLineModel>().Wait();
            _database.CreateTableAsync<R12.Models.DeliveryLineModel>().Wait();
            _database.CreateTableAsync<R12.Models.DeliveryModel>().Wait();
        }
        #region Logging
        public async Task LogAsync(LogMessageModel l)
        {
            await _database.InsertAsync(l);
        }
        public async Task<LogMessageModel> Log_GetAsync(long id)
        {
            return await _database.Table<LogMessageModel>().Where(l => l.LogMessageId == id).FirstOrDefaultAsync();
        }

        public async Task<List<LogMessageModel>> Log_GetAllAsync()
        {
            return await _database.Table<LogMessageModel>().ToListAsync();
        }

        public async Task Log_DeleteAll()
        {
            await _database.ExecuteAsync("DELETE LogMessageModel");
        }
        #endregion

        #region User
        public Task<bool> IsUserLogged()
        {
            throw new NotImplementedException();
        }
        public Task<string> GetUserLogged()
        {
            UserAuthModel userAuth = _database.Table<UserAuthModel>().FirstAsync().Result;
            return Task.FromResult(userAuth.Username);
        }
        public Task<UserAuthModel> GetUserModelLogged()
        {
            var user = _database.Table<UserAuthModel>().FirstOrDefaultAsync().Result;
            if (user != null)
            {
                if (user.Username != null)
                    return Task.FromResult(user);
            }
            return Task.FromResult(new UserAuthModel());
        }
        public async Task UpdateLogin(Andromeda.LoginRequestModel l)
        {
            await _database.DeleteAllAsync<UserAuthModel>();
            await _database.InsertAsync(new UserAuthModel { Username = l.Username, Password = l.Password });
        }
        public async Task UpdateLoginToken(string t)
        {
            UserAuthModel userAuth = await _database.Table<UserAuthModel>().FirstAsync();
            userAuth.Token = t.Trim().Substring(1, t.Length - 2);
            userAuth.TokenDate = DateTime.Now;
            await _database.UpdateAsync(userAuth);
        }
        public async Task<string> GetToken()
        {
            try
            {
                var auth = _database.Table<UserAuthModel>().FirstAsync().Result;
                return auth.Token;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }

        }
        public Task<DateTime> GetTokenDate()
        {
            UserAuthModel userAuth = _database.Table<UserAuthModel>().FirstAsync().Result;
            return Task.FromResult(userAuth.TokenDate);
        }
        public async Task Logout()
        {
            await _database.ExecuteAsync("DELETE FROM DocumentLineModel");
            await _database.ExecuteAsync("DELETE FROM DocumentModel");
        }
        #endregion

        #region Entity
        public async Task SaveEntitiesAsync(List<EntityModel> listE)
        {
            await _database.InsertAllAsync(listE);
        }
        public async Task<List<EntityModel>> GetLocalEntitiesAsync()
        {
            return await _database.Table<EntityModel>().ToListAsync();
        }
        public async Task<EntityModel> GetEntityByIdAsync(int id)
        {
            return await _database.Table<EntityModel>().Where(e => e.EntityId == id).FirstOrDefaultAsync();
        }
        #endregion

        #region Item
        public async Task SaveItemsAsync(List<ItemModel> listI)
        {
            await _database.InsertAllAsync(listI);
        }
        public async Task<ItemModel> GetLocalItemById(int id)
        {
            return await _database.Table<ItemModel>().Where(i => i.ItemId == id).FirstOrDefaultAsync();
        }
        public async Task<ItemModel> GetLocalItemByBarcode(string barcode)
        {
            return await _database.Table<ItemModel>().Where(i => i.ItemBarcode == barcode).FirstOrDefaultAsync();
        }
        public async Task<List<ItemModel>> GetLocalItemsAsync()
        {
            return await _database.Table<ItemModel>().ToListAsync();
        }
        public async Task<List<ItemModel>> GetLocalItemsByCategoryAsync(int id)
        {
            return await _database.Table<ItemModel>().Where(i => i.CategoryId == id && i.ItemDisabled == false).ToListAsync();
        }
        #endregion

        #region Syncro
        public async Task BeforeAllSyncroAsync()
        {
            await _database.ExecuteAsync("DELETE FROM ItemModel");
            await _database.ExecuteAsync("DELETE FROM EntityModel");
            await _database.ExecuteAsync("DELETE FROM CategoryModel");
        }
        public Task DeleteAllSyncro()
        {
            throw new NotImplementedException();
        }
        public async Task<int> DeleteSyncroById(SyncroModel m)
        {
            return await _database.DeleteAsync<SyncroModel>(m.Id);
        }
        public async Task<List<SyncroModel>> GetAllSyncroAsync()
        {
            return await _database.Table<SyncroModel>().ToListAsync();
        }
        public async Task<int> SaveSyncroAsync(SyncroModel m)
        {
            return await _database.InsertAsync(m);
        }
        #endregion

        #region DeliveryTemp
        public async Task<List<DocumentLineModel>> GetTempDelLinesAsync()
        {
            return await _database.Table<DocumentLineModel>().ToListAsync();
        }
        public Task<DocumentLineModel> GetTempDelLineAsync(long id)
        {
            return _database.Table<DocumentLineModel>()
                            .Where(i => i.DocumentLineItemId == id)
                            .FirstOrDefaultAsync();
        }
        public Task<int> DeleteTempDeliveryAsync()
        {
            return _database.ExecuteAsync("DELETE FROM DocumentLineModel");
        }
        public Task<int> DeleteTempDelLineByItemIdAsync(int id)
        {
            return _database.ExecuteAsync("DELETE FROM DocumentLineModel WHERE DocumentLineItemId = " + id.ToString());
        }
        public async Task SaveTempDelLineAsync(DocumentLineModel l)
        {
            DocumentLineModel doc = await _database.Table<DocumentLineModel>().Where(d => d.DocumentLineItemId == l.DocumentLineItemId).FirstAsync();
            if (l.DocumentLineQty > 0)
            {
                doc.DocumentLineQty = l.DocumentLineQty;
                try
                {
                    await _database.UpdateAsync(doc);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                await _database.DeleteAsync(doc);
            }
        }
        public Task<int> AddTempDelLineAsync(DocumentLineModel l)
        {
            if (l.DocumentLineQty > 0 && l.DocumentLineItemId > 0)
                return _database.InsertAsync(l);
            else
                return _database.Table<DocumentLineModel>().DeleteAsync(x => x.DocumentLineItemId == l.DocumentLineItemId);
        }
        public Task<DocumentLineModel> GetTempDelLineByItemIdAsync(int? id)
        {
            return _database.Table<DocumentLineModel>()
                            .Where(i => i.DocumentLineItemId == id)
                            .FirstOrDefaultAsync();
        }
        public Task<DocumentModel> GetTempDelAsync(long id)
        {
            return _database.Table<DocumentModel>()
                            .Where(i => i.DocumentId == id)
                            .FirstOrDefaultAsync();
        }

        #endregion

        #region Delivery
        public async Task<long> SaveLocalDelivery(int eId, List<DeliveryLineModel> l, int status)
        {
            DeliveryModel delivery = new DeliveryModel
            {
                _EntityId = eId,
                _DocumentId = LongRandom(1, 999999, new Random()),
                _DocumentDate = DateTime.Now,
                _DocumentStatus = status
            };
            await _database.InsertAsync(delivery);
            foreach (var sL in l)
            {
                sL._DocumentId = delivery._DocumentId;
                await _database.InsertAsync(sL);
            }
            return (long)delivery._DocumentId;
        }
        public async Task SaveStatusDelivery(long id, int status)
        {
            DeliveryModel delivery = await _database.Table<DeliveryModel>().Where(d => d._DocumentId == id).FirstOrDefaultAsync();
            await _database.ExecuteAsync("UPDATE DeliveryModel SET _DocumentStatus = " + status.ToString().Trim() + " where _DocumentId =" + id.ToString().Trim());
        }
        public async Task SaveNumberDelivery(long id, long number)
        {
            await _database.ExecuteAsync("UPDATE DeliveryModel SET _DocumentNumber = " + number.ToString().Trim() + " where _DocumentId =" + id.ToString().Trim());
        }
        long LongRandom(long min, long max, Random rand)
        {
            byte[] buf = new byte[8];
            rand.NextBytes(buf);
            long longRand = BitConverter.ToInt64(buf, 0);

            return (Math.Abs(longRand % (max - min)) + min);
        }
        public async Task<DeliveryModel> GetLocalDeliveryByIdAsync(long id)
        {
            var delivery = await _database.Table<DeliveryModel>().Where(d => d._DocumentId == id).FirstAsync();
            var deliveryLines = await _database.Table<DeliveryLineModel>().Where(dl => dl._DocumentId == delivery._DocumentId).ToListAsync();
            var deliveryLinesJson = JsonConvert.SerializeObject(deliveryLines);
            delivery.Detail = JsonConvert.DeserializeObject<List<DeliveryLineModel>>(deliveryLinesJson);
            var entityModel = await GetEntityByIdAsync((int)delivery._EntityId);
            delivery._EntityDescription = entityModel.EntityDescription;
            return delivery;
        }
        public async Task<List<DeliveryModel>> GetLocalDeliveriesAsync()
        {
            var deliveries = await _database.Table<DeliveryModel>().ToListAsync();
            var deliveriesJson = JsonConvert.SerializeObject(deliveries);
            var documents = JsonConvert.DeserializeObject<List<DeliveryModel>>(deliveriesJson);
            foreach ( var document in documents)
            {
                var deliveryLines = await _database.Table<DeliveryLineModel>().Where(dl => dl._DocumentId == document._DocumentId).ToListAsync();
                var deliveryLinesJson = JsonConvert.SerializeObject(deliveryLines);
                document.Detail = JsonConvert.DeserializeObject<List<DeliveryLineModel>>(deliveryLinesJson);
                var entityModel = await GetEntityByIdAsync((int)document._EntityId);
                document._EntityDescription = entityModel.EntityDescription;
            }
            return documents;
        }
        public async Task<List<DeliveryModel>> GetLocalDeliveriesAsync(bool pending)
        {
            var deliveries = await _database.Table<DeliveryModel>().Where(dl => dl._DocumentStatus == 0).ToListAsync();
            var deliveriesJson = JsonConvert.SerializeObject(deliveries);
            var documents = JsonConvert.DeserializeObject<List<DeliveryModel>>(deliveriesJson);
            foreach (var document in documents)
            {
                var deliveryLines = await _database.Table<DeliveryLineModel>().Where(dl => dl._DocumentId == document._DocumentId).ToListAsync();
                var deliveryLinesJson = JsonConvert.SerializeObject(deliveryLines);
                document.Detail = JsonConvert.DeserializeObject<List<DeliveryLineModel>>(deliveryLinesJson);
                var entityModel = await GetEntityByIdAsync((int)document._EntityId);
                document._EntityDescription = entityModel.EntityDescription;
            }
            return documents;
        }
        public async Task DeleteAllDeliveriesAsync()
        {
            await _database.ExecuteAsync("DELETE FROM DeliveryModel");
        }
        public async Task DeleteDeliveryAsync(long id)
        {
            await _database.ExecuteAsync("DELETE FROM DeliveryModel where _DocumentId =" + id.ToString());
            await _database.ExecuteAsync("DELETE FROM DeliveryLineModel where _DocumentId =" + id.ToString());
        }
        #endregion

        #region Categories
        public Task SaveCategoriesAsync(List<CategoryModel> c)
        {
            return _database.InsertAllAsync(c);
        }
        public async Task<List<CategoryModel>> GetLocalCategoriesAsync()
        {
            return await _database.Table<CategoryModel>().ToListAsync();
        }

        public async Task<CategoryModel> GetCategoryById(int id)
        {
            return await _database.Table<CategoryModel>().Where(c => c.CategoryId == id).FirstOrDefaultAsync();
        }
        #endregion
    }
}