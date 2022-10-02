using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace R12.Models
{
    public enum ObjectTable
    {
        Document = 1
    }
    public enum CrudAction
    {
        Create = 1,
        Update = 2,
        Delete = 3,
    }
    public class SyncroModel : IComparer<int>
    {
        [JsonProperty(PropertyName = "id")]
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "syncroIntId")]
        public int SyncroIntId { get; set; }

        [JsonProperty(PropertyName = "syncroLongId")]
        public long SyncroLongId { get; set; }

        [JsonProperty(PropertyName = "syncroStringId")]
        public string SyncroStringId { get; set; }

        [JsonProperty(PropertyName = "syncroTargetObject")]
        public ObjectTable SyncroTargetObject { get; set; }

        [JsonProperty(PropertyName = "syncroAction")]
        public CrudAction SyncroAction { get; set; }


        public int Compare(int x, int y)
        {
            if (x == 0 || y == 0)
            {
                return 0;
            }
            // CompareTo() method
            return x.CompareTo(y);
        }
    }
}
