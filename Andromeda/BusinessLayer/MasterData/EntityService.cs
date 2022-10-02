using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.BusinessLayer;
using WebApp.Models;

namespace WebApp.BusinessLayer
{
    public partial class Services : IServices
    {
        public EntityModel Entity_GetById(int id)
        {
            return _repository.Entity_GetById(id);
        }
        public List<EntityModel> Entity_GetAllByType(EntityTypeEnum type)
        {
            return _repository.Entity_GetAllByType(type);
        }
        public ApiResponse Entity_SaveById(EntityModel entity)
        {
            if (_repository.Entity_Update(entity))
                return CustomResponse.Response_Update_Ok((long)entity.EntityId);
            else
                return CustomResponse.Response_Update_Error();
        }
    }
}