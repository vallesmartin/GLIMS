using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class InvLineModel
    {
        [Key, Column(Order = 0)]
        public int InventoryId { get; set; }
        [Key, Column(Order = 1)]
        public int InventoryLineId { get; set; }
        public int InventoryLineItemId { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ItemModel ItemData { get; set; }
        public int InventoryLineQty { get; set; }
        public int InventoryLinePU { get; set; }
        public string InventoryLineUser { get; set; }
        public DateTime InventoryLineDate { get; set; }
    }
}