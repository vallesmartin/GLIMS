using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class DocumentFilter
    {
        public long DocumentId { get; set; }
        public string DocumentLetter { get; set; }
        public long DocumentNumber { get; set; }
        public long DocumentNumberFrom { get; set; }
        public long DocumentNumberTo { get; set; }
        public int EntityId { get; set; }
        public EntityModel EntityData { get; set; }
        public DocumentTypeEnum DocumentType { get; set; }
        public DateTime DocumentDateAt { get; set; }
        public DateTime DocumentDateFrom { get; set; }
        public DateTime DocumentDateTo { get; set; }
        public DateTime DocumentPreparedAt { get; set; }
        public DateTime DocumentBilledAt { get; set; }
        public DateTime DocumentDeliveredAt { get; set; }
        public DateTime DocumentLastUpdateAt { get; set; }
        public DateTime DocumentLastUpdateFrom { get; set; }
        public DateTime DocumentLastUpdateTo { get; set; }
        public DocumentStatusEnum DocumentStatus { get; set; }
        public int DocumentLinesQty { get; set; }
        public int DocumentNumerator { get; set; }
    }
}