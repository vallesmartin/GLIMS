using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
    public class UserModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public bool UserActive { get; set; }
        public int UserLoginTry { get; set; }
        public DateTime UserLastActivity { get; set; }
        [NotMapped]
        public UserRoleModel Role { get; set; }
    }
}