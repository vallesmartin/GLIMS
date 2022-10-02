using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace R12.Models
{
    public class DocumentLineModel
    {
        [Newtonsoft.Json.JsonProperty("DocumentLineId", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? DocumentLineId { get; set; }

        [Newtonsoft.Json.JsonProperty("DocumentId", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public long? DocumentId { get; set; }

        [PrimaryKey]
        [Newtonsoft.Json.JsonProperty("DocumentLineItemId", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public long? DocumentLineItemId { get; set; }

        [Newtonsoft.Json.JsonProperty("DocumentLineItemDescription", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string DocumentLineItemDescription { get; set; }

        [Newtonsoft.Json.JsonProperty("DocumentLineQty", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? DocumentLineQty { get; set; }

        [Newtonsoft.Json.JsonProperty("DocumentLineItemPrice", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public float? DocumentLineItemPrice { get; set; }
    }
}
