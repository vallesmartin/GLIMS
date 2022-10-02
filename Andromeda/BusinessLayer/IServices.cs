using System.Collections.Generic;
using WebApp.Models;

namespace WebApp.BusinessLayer
{
    interface IServices
    {
        #region Login
        ApiResponse Login(LoginRequestModel login);
        #endregion

        #region Entity
        EntityModel Entity_GetById(int id);
        List<EntityModel> Entity_GetAllByType(EntityTypeEnum type);
        ApiResponse Entity_SaveById(EntityModel entity);
        #endregion

        #region Item
        ItemModel Item_GetById(int id);
        ApiResponse Item_SaveById(ItemModel item);
        ApiResponse Item_Disable(int id);
        List<ItemModel> Item_GetAll();
        ApiResponse Item_AssignBarcode(ItemModel item);
        #endregion

        #region Document
        DocumentModel Document_GetById(long id);
        List<DocumentModel> Document_GetByFilter(DocumentFilter filter);
        List<DocumentModel> Document_GetAllByStatus(DocumentStatusEnum status);
        List<DocumentModel> Document_GetByActivity(int limit);
        ApiResponse Document_Create(DocumentModel document);
        ApiResponse Document_SetPrepared(DocumentModel document);
        ApiResponse Document_SetBilled(DocumentModel document);
        ApiResponse Document_SetDelivered(DocumentModel document);
        ApiResponse Document_SetSigned(DocumentModel document);
        ApiResponse Document_SignedDelivered(DocumentModel document);
        ApiResponse Document_UpdateLine(DocumentLineModel line);
        DocumentModel Document_GetLast();
        #endregion

        #region Category
        List<CategoryModel> Category_GetAll();
        ApiResponse Category_Create(CategoryModel category);
        CategoryModel Category_UpdateOrder(CategoryModel category);
        #endregion

        #region User
        List<UserModel> User_GetAll();
        UserModel User_GetById(int id);
        UserRoleModel UserRole_Get(string user);
        ApiResponse User_New(UserModel user);
        ApiResponse UserRole_Set(UserRoleModel role);
        ApiResponse User_ChangePassword(UserModel user);
        #endregion
        
        #region Inventory
        ApiResponse Inventory_Create(InvModel inventory);
        InvModel Inventory_GetOpen();
        ApiResponse InventoryLine_Create(InvLineModel line);
        #endregion
    }
}
