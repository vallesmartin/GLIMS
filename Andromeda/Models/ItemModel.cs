using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class ItemModel
    {
        [Key]
        public int ItemId { get; set; }
        public string ItemDescription { get; set; }
        public string ItemExternalCode { get; set; }
        public string ItemInternalCode { get; set; }
        public string ItemInternalDescription { get; set; }
        public string ItemBarcode { get; set; }
        public float ItemPrice { get; set; }
        public float ItemCost { get; set; }
        public int ItemSugg { get; set; }
        public int ItemSuggLow { get; set; }
        public int ItemSuggHigh { get; set; }
        public int ItemSuggStep { get; set; }
        public int ItemOrder { get; set; }
        public int ItemPackUnit { get; set; }
        public int CategoryId { get; set; }
        public CategoryModel CategoryData { get; set; }
        public int EntityId { get; set; }
        public EntityModel EntityData { get; set; }
        public bool ItemDisabled { get; set; }
    }
}