using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace R12.Models
{
    public class CategoryModel
    {
        [PrimaryKey]
        [Newtonsoft.Json.JsonProperty("CategoryId", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]

        public int CategoryId { get; set; }

        [Newtonsoft.Json.JsonProperty("CategoryDescription", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]

        public string CategoryDescription { get; set; }

        [Newtonsoft.Json.JsonProperty("CategoryOrder", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? CategoryOrder { get; set; }

        [Newtonsoft.Json.JsonProperty("CategoryImageName", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string CategoryImageName { get; set; }
    }
}
