using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace R12.Models
{
        public class EntityModel
        {
            [Newtonsoft.Json.JsonProperty("EntityId", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
            [PrimaryKey]
            public int? EntityId { get; set; }

            [Newtonsoft.Json.JsonProperty("EntityDescription", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
            public string EntityDescription { get; set; }

            [Newtonsoft.Json.JsonProperty("EntityLegalName", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
            public string EntityLegalName { get; set; }

            [Newtonsoft.Json.JsonProperty("EntityAddress", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
            public string EntityAddress { get; set; }

            [Newtonsoft.Json.JsonProperty("EntityLocation", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
            public string EntityLocation { get; set; }

            [Newtonsoft.Json.JsonProperty("EntityContact", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
            public string EntityContact { get; set; }

            [Newtonsoft.Json.JsonProperty("EntityPhone", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
            public string EntityPhone { get; set; }

            [Newtonsoft.Json.JsonProperty("EntityMail", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
            public string EntityMail { get; set; }

            [Newtonsoft.Json.JsonProperty("EntityInternalCode", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
            public string EntityInternalCode { get; set; }

            [Newtonsoft.Json.JsonProperty("EntityType", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
            public int EntityType { get; set; }
    }
}
