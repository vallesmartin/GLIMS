using R12.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace R12.Services
{
    public interface IDataStore
    {
        #region Logging
        Task LogAsync(LogMessageModel l);
        Task<LogMessageModel> Log_GetAsync(long id);
        Task<List<LogMessageModel>> Log_GetAllAsync();
        Task Log_DeleteAll();
        #endregion

        #region User
        Task<string> GetUserLogged();
        Task<UserAuthModel> GetUserModelLogged();
        Task UpdateLogin(Andromeda.LoginRequestModel l);
        Task UpdateLoginToken(string t);
        Task<string> GetToken();
        Task<DateTime> GetTokenDate();
        Task Logout();
        #endregion

        #region Entity
        Task SaveEntitiesAsync(List<EntityModel> e);
        Task<List<EntityModel>> GetLocalEntitiesAsync();
        Task<EntityModel> GetEntityByIdAsync(int id);
        #endregion

        #region Item
        Task SaveItemsAsync(List<ItemModel> i);
        Task<ItemModel> GetLocalItemById(int id);
        Task<ItemModel> GetLocalItemByBarcode(string barcode);
        Task<List<ItemModel>> GetLocalItemsAsync();
        Task<List<ItemModel>> GetLocalItemsByCategoryAsync(int id);
        #endregion

        #region Syncro
        Task BeforeAllSyncroAsync();
        Task<List<SyncroModel>> GetAllSyncroAsync();
        Task<int> DeleteSyncroById(SyncroModel m);
        Task DeleteAllSyncro();
        Task<int> SaveSyncroAsync(SyncroModel m);
        #endregion

        #region Delivery
        Task<int> AddTempDelLineAsync(DocumentLineModel l);
        Task<List<DocumentLineModel>> GetTempDelLinesAsync();
        Task<DocumentModel> GetTempDelAsync(long id);
        Task<DocumentLineModel> GetTempDelLineAsync(long id);
        Task<DocumentLineModel> GetTempDelLineByItemIdAsync(int? id);
        Task<int> DeleteTempDeliveryAsync();
        Task DeleteAllDeliveriesAsync();
        Task DeleteDeliveryAsync(long id);
        Task<int> DeleteTempDelLineByItemIdAsync(int id);
        Task SaveTempDelLineAsync(DocumentLineModel l);
        Task<DeliveryModel> GetLocalDeliveryByIdAsync(long id);
        Task<List<DeliveryModel>> GetLocalDeliveriesAsync();
        Task<List<DeliveryModel>> GetLocalDeliveriesAsync(bool pending);
        Task<long> SaveLocalDelivery(int eId, List<DeliveryLineModel> l, int status);
        Task SaveStatusDelivery(long id, int status);
        Task SaveNumberDelivery(long id, long number);
        #endregion

        #region Categories
        Task SaveCategoriesAsync(List<CategoryModel> c);
        Task<List<CategoryModel>> GetLocalCategoriesAsync();
        Task<CategoryModel> GetCategoryById(int id);
        #endregion
    }
}
