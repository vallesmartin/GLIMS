using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace R12.Common
{
    public class APIResponse
    {
        public bool IsOk { get; set; }
        public string Message { get; set; }
        public string NumericMessage { get; set; }
    }
}