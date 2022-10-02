using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace R12.Models
{
    public class DeliveryModel
    {
        [PrimaryKey]
        [Newtonsoft.Json.JsonProperty("DocumentId", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public long? _DocumentId { get; set; }

        [Newtonsoft.Json.JsonProperty("DocumentLetter", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string _DocumentLetter { get; set; }

        [Newtonsoft.Json.JsonProperty("DocumentNumber", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public long? _DocumentNumber { get; set; }

        [Newtonsoft.Json.JsonProperty("EntityId", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? _EntityId { get; set; }

        [Newtonsoft.Json.JsonProperty("EntityDescription", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string _EntityDescription { get; set; }

        [Newtonsoft.Json.JsonProperty("DocumentDate", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.DateTimeOffset? _DocumentDate { get; set; }

        [Newtonsoft.Json.JsonProperty("DocumentPreparedAt", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.DateTimeOffset? _DocumentPreparedAt { get; set; }

        [Newtonsoft.Json.JsonProperty("DocumentBilledAt", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.DateTimeOffset? _DocumentBilledAt { get; set; }

        [Newtonsoft.Json.JsonProperty("DocumentDeliveredAt", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.DateTimeOffset? _DocumentDeliveredAt { get; set; }

        [Newtonsoft.Json.JsonProperty("DocumentStatus", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int _DocumentStatus { get; set; }

        public string _DocumentStatusText 
        {
            get
            {
                string type;
                switch (this._DocumentStatus)
                {
                    case 0:
                        type = "Pendiente de envío";
                        break;
                    default:
                        type = "Enviado";
                        break;
                }
                return type;
            }
        }

        [Newtonsoft.Json.JsonProperty("DocumentTotalAmount", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public float? _DocumentTotalAmount { get; set; }

        [Newtonsoft.Json.JsonProperty("DocumentNumerator", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int _DocumentNumerator { get; set; }
        [Newtonsoft.Json.JsonProperty("DocumentType", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int _DocumentType { get; set; }

        [Newtonsoft.Json.JsonProperty("DocumentLinesQty", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? _DocumentLinesQty { get; set; }
        [Newtonsoft.Json.JsonProperty("Detail", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [Ignore]
        public System.Collections.Generic.ICollection<DeliveryLineModel> Detail { get; set; }
        [Ignore]
        public Color _color { get; set; }
        [Ignore]
        public bool _numberVisible { get; set; }
    }
}
