using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models

{
    public class ApiResponse
    {
        public bool isOk { get; set; }
        public string Message { get; set; }
        public long NumericMessage { get; set; }
    }
}