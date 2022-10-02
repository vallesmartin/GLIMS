using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.DataAccess
{
    public partial class Repository : IRepository
    {
        public List<EntityModel> Entity_GetAllByType(EntityTypeEnum type)
        {
            using (var _context = new AndromedaContext())
            {
                return _context.Entities.Where(e => e.EntityType == type).ToList();
            }
        }
        public EntityModel Entity_GetById(int id)
        {
            using (var _context = new AndromedaContext())
            {
                return _context.Entities.Where(e => e.EntityId == id).First();
            }
        }
        public bool Entity_Update(EntityModel entity)
        {
            using (var _context = new AndromedaContext())
            {
                var dbEnt = _context.Entities.Where(e => e.EntityId == entity.EntityId).First();
                dbEnt.EntityAddress = entity.EntityAddress.Length > 0 ? entity.EntityAddress : dbEnt.EntityAddress;
                dbEnt.EntityContact = entity.EntityContact.Length > 0 ? entity.EntityContact : dbEnt.EntityContact;
                dbEnt.EntityDescription = entity.EntityDescription.Length > 0 ? entity.EntityDescription : dbEnt.EntityDescription;
                dbEnt.EntityInternalCode = entity.EntityInternalCode.Length > 0 ? entity.EntityInternalCode : dbEnt.EntityInternalCode;
                dbEnt.EntityLegalName = entity.EntityLegalName.Length > 0 ? entity.EntityLegalName : dbEnt.EntityLegalName;
                dbEnt.EntityLocation = entity.EntityLocation.Length > 0 ? entity.EntityLocation : dbEnt.EntityLocation;
                dbEnt.EntityMail = entity.EntityMail.Length > 0 ? entity.EntityMail : dbEnt.EntityMail;
                dbEnt.EntityPhone = entity.EntityPhone.Length > 0 ? entity.EntityPhone : dbEnt.EntityPhone;

                _context.SaveChanges();
                Changes_Set(AndromedaContextObject.OBJ_ENTITY);
                return true;
            }
        }
    }
}