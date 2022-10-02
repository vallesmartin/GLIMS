using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class InvModel
    {
        [Key]
        public int InventoryId { get; set; }
        public string InventoryDescription { get; set; }
        public InventoryTypeEnum InventoryType { get; set; }
        public InventoryStatusEnum InventoryStatus { get; set; }
        public DateTime InventoryCreationDate { get; set; }
        public DateTime InventoryEndDate { get; set; }
        public List<InvLineModel> Detail { get; set; }
    }

    public enum InventoryStatusEnum
    {
        GENERATED = 1,
        REVISION = 2,
        FINALIZED = 3
    }

    public enum InventoryTypeEnum
    {
        GENERAL = 1,
        PARTIAL = 2
    }
}