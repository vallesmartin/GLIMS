using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.BusinessLayer
{
    public partial class Services : IServices
    {
        public ApiResponse Log_Create(LogModel log)
        {
            log.LogAt = DateTime.Now;
            try
            {
                long id = _repository.Log_Create(log);
                return CustomResponse.Response_Update_Ok(id);
            }
            catch (Exception ex)
            {
                return CustomResponse.Response_Exception("LoggingCreate_", ex);
            }
        }
    }
}