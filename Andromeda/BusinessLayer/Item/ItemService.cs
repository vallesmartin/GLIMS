using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.BusinessLayer
{
    public partial class Services : IServices
    {
        public ItemModel Item_GetById(int id)
        {
            return _repository.Item_GetById(id);
        }
        public List<ItemModel> Item_GetAll()
        {
            return _repository.Item_GetAll();
        }
        public ApiResponse Item_SaveById(ItemModel item)
        {
            if (_repository.Item_Update(item))
                return CustomResponse.Response_Update_Ok((long)item.ItemId);
            else
                return CustomResponse.Response_Update_Error();
        }
        public ApiResponse Item_Disable(int id)
        {
            ApiResponse response;
            ItemModel item;
            try
            {
                item = Item_GetById(id);
                if (item.ItemId > 0)
                {
                    item.ItemDisabled = true;
                    response = Item_SaveById(item);
                    return CustomResponse.Response_Update_Ok((long)item.ItemId);
                }
                else
                {
                    return CustomResponse.Response_Update_Error();
                }
            }
            catch (Exception ex)
            {
                return CustomResponse.Response_Exception("Item_Disable_", ex);
            }
        }
        public ApiResponse Item_AssignBarcode(ItemModel item)
        {
            var itemShell = Item_GetById(item.ItemId);
            itemShell.ItemBarcode = item.ItemBarcode;
            if (_repository.Item_Update(itemShell))
                return CustomResponse.Response_Update_Ok((long)item.ItemId);
            else
                return CustomResponse.Response_Update_Error();
        }
    }
}