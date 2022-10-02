using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class DocumentLineModel
    {
        [Key, Column(Order = 0)]
        public int DocumentLineId { get; set; }
        [Key, Column(Order = 1)]
        public long DocumentId { get; set; }
        public long DocumentLineItemId { get; set; }
        public string DocumentLineItemDescription { get; set; }
        public int DocumentLineQty { get; set; }
        public float DocumentLineItemPrice { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ItemModel ItemData{ get; set; }
    }
}