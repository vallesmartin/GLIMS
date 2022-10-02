using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.BusinessLayer
{
    public partial class Services : IServices
    {
        public List<CategoryModel> Category_GetAll()
        {
            return _repository.Category_GetAll();
        }
        public ApiResponse Category_Create(CategoryModel category)
        {
            try
            {
                var id = _repository.Category_Create(category);
                if (id > 0)
                    return CustomResponse.Response_Update_Ok(id);
                else
                    return CustomResponse.Response_Update_Error();
            }
            catch (Exception ex)
            {
                return CustomResponse.Response_Exception("CatCreate_", ex);
            }
        }
        public CategoryModel Category_UpdateOrder(CategoryModel category)
        {
            throw new NotImplementedException();
        }
    }
}