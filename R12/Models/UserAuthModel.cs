using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace R12.Models
{
    public class UserAuthModel
    {
        [JsonProperty(PropertyName = "Username")]
        [PrimaryKey]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "Password")]
        public string Password{ get; set; }

        [JsonProperty(PropertyName = "Token")]
        public string Token{ get; set; }

        [JsonProperty(PropertyName = "TokenDate")]
        public DateTime TokenDate { get; set; }
    }
}
