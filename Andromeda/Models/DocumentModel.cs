using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class DocumentModel
    {
        [Key]
        public long DocumentId { get; set; }
        public string DocumentLetter { get; set; }
        public long DocumentNumber { get; set; }
        public int EntityId { get; set; }
        public EntityModel EntityData { get; set; }
        public DocumentTypeEnum DocumentType { get; set; }
        public DateTime DocumentDate { get; set; }
        public DateTime DocumentPreparedAt { get; set; }
        public DateTime DocumentBilledAt { get; set; }
        public DateTime DocumentDeliveredAt { get; set; }
        public DateTime DocumentLastUpdateAt { get; set; }
        public DocumentStatusEnum DocumentStatus { get; set; }
        public float DocumentTotalAmount { get; set; }
        public int DocumentLinesQty { get; set; }
        public List<DocumentLineModel> Detail { get; set; }
        public int DocumentNumerator { get; set; }
    }
    public enum DocumentTypeEnum
    {
        DELIVERY = 0,
        ENTRY = 1
    }
    public enum DocumentStatusEnum
    {
        GENERATED = 1,
        PREPARED = 10,
        BILLED = 20, 
        DELIVERED = 30,
        DELIVERED_AND_NOT_INCOME = 31,
        CANCELLED = 50,
        REENTRY = 51
    }
}