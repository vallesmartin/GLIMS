using System;
using System.Collections.Generic;
using System.Text;

namespace R12.Models
{
    class UserRoleModel
    {
        [Newtonsoft.Json.JsonProperty("UserId", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? UserId { get; set; }

        [Newtonsoft.Json.JsonProperty("UserRoleIsSeller", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool? UserRoleIsSeller { get; set; }

        [Newtonsoft.Json.JsonProperty("UserRoleIsPicker", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool? UserRoleIsPicker { get; set; }

        [Newtonsoft.Json.JsonProperty("UserRoleIsBiller", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool? UserRoleIsBiller { get; set; }

        [Newtonsoft.Json.JsonProperty("UserRoleIsDeliverer", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool? UserRoleIsDeliverer { get; set; }

        [Newtonsoft.Json.JsonProperty("UserRoleIsAdmin", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool? UserRoleIsAdmin { get; set; }

        [Newtonsoft.Json.JsonProperty("UserRoleIsSuperAdmin", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool? UserRoleIsSuperAdmin { get; set; }
    }
}
