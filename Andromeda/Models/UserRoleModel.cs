using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class UserRoleModel
    {
        [Key]
        public int UserId { get; set; }
        public bool UserRoleIsSeller { get; set; }
        public bool UserRoleIsPicker { get; set; }
        public bool UserRoleIsBiller { get; set; }
        public bool UserRoleIsDeliverer { get; set; }
        public bool UserRoleIsAdmin { get; set; }
        public bool UserRoleIsSuperAdmin { get; set; }
    }
}