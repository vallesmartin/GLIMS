using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace R12.Models
{
    public class DeliveryLineModel
    {
        [PrimaryKey, AutoIncrement]
        [Newtonsoft.Json.JsonProperty("DocumentLineId", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? _DocumentLineId { get; set; }

        [Newtonsoft.Json.JsonProperty("DocumentId", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public long? _DocumentId { get; set; }

        [Newtonsoft.Json.JsonProperty("DocumentLineItemId", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public long? _DocumentLineItemId { get; set; }

        [Newtonsoft.Json.JsonProperty("DocumentLineItemDescription", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string _DocumentLineItemDescription { get; set; }

        [Newtonsoft.Json.JsonProperty("DocumentLineQty", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? _DocumentLineQty { get; set; }

        [Newtonsoft.Json.JsonProperty("DocumentLineItemPrice", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public float? _DocumentLineItemPrice { get; set; }
    }
}
