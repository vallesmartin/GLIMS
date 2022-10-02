using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class EntityModel
    {
        [Key]
        public int EntityId { get; set; }
        public string EntityDescription { get; set; }
        public string EntityLegalName { get; set; }
        public string EntityAddress { get; set; }
        public string EntityLocation { get; set; }
        public string EntityContact { get; set; }
        public string EntityPhone { get; set; }
        public string EntityMail { get; set; }
        public string EntityInternalCode { get; set; }
        public EntityTypeEnum EntityType { get; set; }
    }
    public enum EntityTypeEnum
    {
        CUSTOMER = 1,
        SUPPLIER = 2,
        WAREHOUSE = 3
    }
}