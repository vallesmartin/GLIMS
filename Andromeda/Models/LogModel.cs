using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class LogModel
    {
        [Key]
        public long LogId { get; set; }
        public DateTime LogAt { get; set; }
        public string LogCode { get; set; }
        public string LogDescription { get; set; }
        public long LogValue { get; set; }
        public string LogUser { get; set; }
        public string LogOldValue { get; set; }
        public string LogNewValue { get; set; }

    }
}