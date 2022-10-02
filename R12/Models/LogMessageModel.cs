using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace R12.Models
{
    public class LogMessageModel
    {
        [PrimaryKey, AutoIncrement]
        public long LogMessageId { get; set; }
        public int LogMessageLevel { get; set; }
        public long LogId { get; set; }
        public string LogCode { get; set; }
        public string LogUser { get; set; }
        public string LogDescription { get; set; }
        public string LogMessageException { get; set; }
        public DateTime LogAt { get; set; }
    }
}
