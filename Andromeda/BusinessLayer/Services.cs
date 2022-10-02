using System;
using System.Collections.Generic;
using WebApp.BusinessLayer;
using WebApp.DataAccess;
using WebApp.Models;

namespace WebApp.BusinessLayer
{
    public partial class Services : IServices
    {
        private readonly IRepository _repository;

        public Services(IRepository repository)
        {
            _repository = repository;
        }

    }

    public static class CustomResponse
    {
        public static ApiResponse Response_Initialize()
        {
            return new ApiResponse { isOk = false, Message = "", NumericMessage = 0 };
        }
        public static ApiResponse Response_Update_Ok(long key)
        {
            return new ApiResponse { isOk = true, Message = "Registro actualizado exitosamente.", NumericMessage = key };
        }
        public static ApiResponse Response_Update_Error()
        {
            return new ApiResponse { isOk = false, Message = "Error al actualizar el registro.", NumericMessage = 0 };
        }
        public static ApiResponse Response_NullKey_Error()
        {
            return new ApiResponse { isOk = false, Message = "No hay registro para actualizar.", NumericMessage = 0 };
        }
        public static ApiResponse Response_Exception(string code, Exception ex)
        {
            return new ApiResponse { isOk = false, Message = code+'-'+ex.Message, NumericMessage = 0 };
        }
    }
}