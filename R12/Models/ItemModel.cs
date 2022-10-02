using System;
using System.Collections.Generic;
using System.Text;

namespace R12.Models
{
    public partial class ItemModel
    {
        [Newtonsoft.Json.JsonProperty("ItemId", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? ItemId { get; set; }

        [Newtonsoft.Json.JsonProperty("ItemDescription", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string ItemDescription { get; set; }

        [Newtonsoft.Json.JsonProperty("ItemExternalCode", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string ItemExternalCode { get; set; }

        [Newtonsoft.Json.JsonProperty("ItemInternalCode", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string ItemInternalCode { get; set; }

        [Newtonsoft.Json.JsonProperty("ItemInternalDescription", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string ItemInternalDescription { get; set; }

        [Newtonsoft.Json.JsonProperty("ItemBarcode", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string ItemBarcode { get; set; }

        [Newtonsoft.Json.JsonProperty("ItemPrice", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public float? ItemPrice { get; set; }

        [Newtonsoft.Json.JsonProperty("ItemCost", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public float? ItemCost { get; set; }

        [Newtonsoft.Json.JsonProperty("ItemSugg", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? ItemSugg { get; set; }

        [Newtonsoft.Json.JsonProperty("ItemSuggLow", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? ItemSuggLow { get; set; }

        [Newtonsoft.Json.JsonProperty("ItemSuggHigh", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? ItemSuggHigh { get; set; }

        [Newtonsoft.Json.JsonProperty("ItemSuggStep", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? ItemSuggStep { get; set; }

        [Newtonsoft.Json.JsonProperty("ItemOrder", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? ItemOrder { get; set; }

        [Newtonsoft.Json.JsonProperty("ItemPackUnit", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int ItemPackUnit { get; set; }

        [Newtonsoft.Json.JsonProperty("CategoryId", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? CategoryId { get; set; }

        [Newtonsoft.Json.JsonProperty("EntityId", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? EntityId { get; set; }

        [Newtonsoft.Json.JsonProperty("ItemDisabled", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool ItemDisabled { get; set; }

    }
}
