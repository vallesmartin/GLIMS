using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.BusinessLayer
{
    public partial class Services : IServices
    {
        public ApiResponse Login(LoginRequestModel login)
        {
            if (_repository.Login(login))
                return CustomResponse.Response_Update_Ok(0);
            else
                return CustomResponse.Response_Update_Error();
        }
    }
}