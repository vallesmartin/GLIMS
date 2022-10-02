using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace R12.Models
{
    public class DocumentModel
    {
        [Newtonsoft.Json.JsonProperty("DocumentId", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public long? DocumentId { get; set; }

        [Newtonsoft.Json.JsonProperty("DocumentLetter", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string DocumentLetter { get; set; }

        [Newtonsoft.Json.JsonProperty("DocumentNumber", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public long? DocumentNumber { get; set; }

        [Newtonsoft.Json.JsonProperty("EntityId", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? EntityId { get; set; }

        [Newtonsoft.Json.JsonProperty("EntityData", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public EntityModel EntityModel { get; set; }

        [Newtonsoft.Json.JsonProperty("DocumentDate", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.DateTimeOffset? DocumentDate { get; set; }

        [Newtonsoft.Json.JsonProperty("DocumentPreparedAt", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.DateTimeOffset? DocumentPreparedAt { get; set; }

        [Newtonsoft.Json.JsonProperty("DocumentBilledAt", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.DateTimeOffset? DocumentBilledAt { get; set; }

        [Newtonsoft.Json.JsonProperty("DocumentDeliveredAt", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.DateTimeOffset? DocumentDeliveredAt { get; set; }

        [Newtonsoft.Json.JsonProperty("DocumentLastUpdateAt", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.DateTimeOffset? DocumentLastUpdateAt { get; set; }

        [Newtonsoft.Json.JsonProperty("DocumentStatus", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int DocumentStatus { get; set; }

        [Newtonsoft.Json.JsonProperty("DocumentTotalAmount", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public float? DocumentTotalAmount { get; set; }

        [Newtonsoft.Json.JsonProperty("DocumentNumerator", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int DocumentNumerator { get; set; }

        [Newtonsoft.Json.JsonProperty("DocumentType", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int DocumentType { get; set; }

        [Newtonsoft.Json.JsonProperty("DocumentLinesQty", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? DocumentLinesQty { get; set; }

        [Newtonsoft.Json.JsonProperty("Detail", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.ICollection<DocumentLineModel> Detail { get; set; }


    }
}
