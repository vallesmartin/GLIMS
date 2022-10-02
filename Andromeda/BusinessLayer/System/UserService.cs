using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.BusinessLayer
{
    public partial class Services : IServices
    {
        public List<UserModel> User_GetAll()
        {
            return _repository.User_GetAll();
        }
        public UserModel User_GetById(int id)
        {
            return _repository.User_GetById(id);
        }
        public ApiResponse User_New(UserModel user)
        {
            if (_repository.User_New(user))
                return CustomResponse.Response_Update_Ok(0);
            else
                return CustomResponse.Response_Update_Error();
        }
        public UserRoleModel UserRole_Get(string user)
        {
            return _repository.UserRole_Get(user);
        }
        public ApiResponse UserRole_Set(UserRoleModel role)
        {
            if (_repository.UserRole_Set(role))
                return CustomResponse.Response_Update_Ok(0);
            else
                return CustomResponse.Response_Update_Error();
        }
        public ApiResponse User_ChangePassword(UserModel user)
        {
            if (_repository.User_SavePassword(user))
                return CustomResponse.Response_Update_Ok(0);
            else
                return CustomResponse.Response_Update_Error();
        }
    }
}