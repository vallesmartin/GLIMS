using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.DataAccess
{
    public interface IRepository
    {
        List<ChangeModel> Changes_Get();
        ChangeModel Changes_GetLastForSynchro();

        #region Login
        bool Login(LoginRequestModel login);
        #endregion

        #region Entity
        EntityModel Entity_GetById(int id);
        List<EntityModel> Entity_GetAllByType(EntityTypeEnum type);
        bool Entity_Update(EntityModel entity);
        #endregion

        #region Item
        ItemModel Item_GetById(int id);
        List<ItemModel> Item_GetAll();
        bool Item_Update(ItemModel item);
        #endregion

        #region Document3
        List<DocumentModel> Document_ByFilter(DocumentFilter filter);
        DocumentModel Document_GetById(long id);
        DocumentModel Document_GetLastByDate(DocumentTypeEnum type);
        List<DocumentModel> Document_GetAllByStatus(DocumentTypeEnum type, DocumentStatusEnum status);
        List<DocumentModel> Document_GetAlive();
        long Document_Create(DocumentModel document);
        bool Document_Update(DocumentModel document);
        List<DocumentLineModel> DocumentLine_GetListByDocumentId(long id);
        bool DocumentLine_Update(DocumentLineModel line);
        #endregion

        #region Category
        List<CategoryModel> Category_GetAll();
        int Category_Create(CategoryModel category);
        #endregion

        #region User
        List<UserModel> User_GetAll();
        UserModel User_GetById(int id);
        bool User_New(UserModel user);
        UserRoleModel UserRole_Get(string user);
        bool UserRole_Set(UserRoleModel role);
        bool User_SavePassword(UserModel user);
        #endregion
        
        #region Inventory
        int Inventory_Create(InvModel inventory);
        InvModel Inventory_GetById(int id);
        InvModel Inventory_GetOpen();
        List<InvModel> Inventory_GetAll();
        bool Inventory_UpdateById(InvModel inventory);
        List<InvLineModel> InventoryLine_GetLinesByInventory(int id);
        InvLineModel InventoryLine_GetById(int inventoryId, int id);
        int InventoryLine_Create(InvLineModel line);
        bool InventoryLine_UpdateById(InvLineModel line);

        #endregion

        #region Logging
        long Log_Create(LogModel log);
        #endregion
    }
}
