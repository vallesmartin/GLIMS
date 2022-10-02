using System;
using System.Collections.Generic;
using System.Text;

namespace R12.Services
{
    public class ApiResponse
    {
        [Newtonsoft.Json.JsonProperty("isOk", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool isOk { get; set; }

        [Newtonsoft.Json.JsonProperty("Message", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Message { get; set; }

        [Newtonsoft.Json.JsonProperty("NumericMessage", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public long NumericMessage { get; set; }
    }
}
