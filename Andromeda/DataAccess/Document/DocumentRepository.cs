using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.DataAccess
{
    public partial class Repository : IRepository
    {
        public List<DocumentModel> Document_ByFilter(DocumentFilter filter)
        {
            using (var _context = new AndromedaContext())
            {
                var documents = _context.Documents.Where(d => 
                                                ( d.DocumentStatus == filter.DocumentStatus || filter.DocumentStatus == 0) &&
                                                ( d.EntityId == filter.EntityId || filter.EntityId == 0) &&
                                                ( d.DocumentNumber == filter.DocumentNumber || filter.DocumentNumber == 0) &&
                                                ( DbFunctions.TruncateTime(d.DocumentLastUpdateAt) >= DbFunctions.TruncateTime(filter.DocumentLastUpdateAt))
                                                
                                                )
                    .ToList();
                foreach (var document in documents)
                    document.EntityData = Entity_GetById(document.EntityId);
                return documents;
            }
        }
        public long Document_Create(DocumentModel document)
        {
            using (var _context = new AndromedaContext())
            {
                var numCode = "";
                switch (document.DocumentType)
                {
                    case DocumentTypeEnum.DELIVERY:
                        numCode = "DELIVERY";
                        break;
                    case DocumentTypeEnum.ENTRY:
                        numCode = "ENTRY";
                        break;
                    default:
                        numCode = "DEFAULT";
                        break;
                }

                var tempDoc = new DocumentModel
                {
                    DocumentDate = DateTime.Now,
                    DocumentPreparedAt = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue,
                    DocumentBilledAt = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue,
                    DocumentDeliveredAt = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue,
                    DocumentLastUpdateAt = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue,
                    DocumentLetter = document.DocumentLetter,
                    DocumentLinesQty = document.DocumentLinesQty,
                    DocumentNumber = Numerator_GetLast(numCode),
                    DocumentStatus = document.DocumentStatus,
                    DocumentTotalAmount = document.DocumentTotalAmount,
                    DocumentType = document.DocumentType,
                    EntityId = document.EntityId
                };
                _context.Documents.Add(tempDoc);
                _context.SaveChanges();
                int counter = 1;
                foreach (var line in document.Detail)
                {
                    line.DocumentId = tempDoc.DocumentId;
                    line.DocumentLineId = counter;
                    counter++;
                }
                _context.DocumentLines.AddRange(document.Detail);

                var result = _context.Documents.SingleOrDefault(d => d.DocumentId == tempDoc.DocumentId);

                _context.SaveChanges();
                return tempDoc.DocumentId;
            }
        }
        private long Numerator_GetLast(string numCode)
        {
            using (var _context = new AndromedaContext())
            {
                try
                {
                    var numerator = _context.Numerator.Where(n => n.NumeratorCode == numCode).First();
                    numerator.NumeratorLast++;
                    _context.SaveChanges();
                    return numerator.NumeratorLast;
                }
                catch (Exception ex)
                {
                    NumeratorsModel numerator = new NumeratorsModel
                    {
                        NumeratorCode = numCode,
                        NumeratorLast = 1
                    };
                    _context.Numerator.Add(numerator);
                    return numerator.NumeratorLast;
                }
            }
        }
        public DocumentModel Document_GetLastByDate(DocumentTypeEnum type)
        {
            using (var _context = new AndromedaContext())
            {
                return _context.Documents.Where(d => d.DocumentType == type).ToList().OrderByDescending(d => d.DocumentDate).FirstOrDefault();
            }
        }
        public bool Document_Update(DocumentModel document)
        {
            using (var _context = new AndromedaContext())
            {
                var origRecord = _context.Documents.Where(d => d.DocumentId == document.DocumentId).FirstOrDefault();
                origRecord.DocumentLetter = document.DocumentLetter;
                origRecord.DocumentNumber = document.DocumentNumber;
                origRecord.DocumentStatus = document.DocumentStatus;
                origRecord.DocumentTotalAmount = document.DocumentTotalAmount;
                origRecord.DocumentLinesQty = document.DocumentLinesQty;
                origRecord.DocumentNumerator = document.DocumentNumerator;
                origRecord.DocumentLastUpdateAt = document.DocumentLastUpdateAt;

                _context.SaveChanges();
                Changes_Set(AndromedaContextObject.OBJ_DOCUMENTS);
                return true;
            }
        }
        public List<DocumentModel> Document_GetAllByStatus(DocumentTypeEnum type, DocumentStatusEnum status)
        {
            using (var _context = new AndromedaContext())
            {
                var documents = _context.Documents.Where(d => d.DocumentStatus == status && d.DocumentType == type).ToList();
                foreach (var document in documents)
                    document.EntityData = Entity_GetById(document.EntityId);
                return documents;
            }
        }
        public List<DocumentModel> Document_GetAlive()
        {
            using (var _context = new AndromedaContext())
            {
                return _context.Documents.Where(d => (d.DocumentStatus == DocumentStatusEnum.GENERATED ||
                                                 d.DocumentStatus == DocumentStatusEnum.PREPARED ||
                                                 d.DocumentStatus == DocumentStatusEnum.BILLED ||
                                                 d.DocumentStatus == DocumentStatusEnum.DELIVERED ||
                                                 d.DocumentStatus == DocumentStatusEnum.DELIVERED_AND_NOT_INCOME) && d.DocumentType == DocumentTypeEnum.DELIVERY).OrderByDescending(d => d.DocumentLastUpdateAt).ToList();
            }
        }
        public DocumentModel Document_GetById(long id)
        {
            using (var _context = new AndromedaContext())
            {
                var document = _context.Documents.Where(d => d.DocumentId == id).FirstOrDefault();
                document.EntityData = Entity_GetById(document.EntityId);
                document.Detail = DocumentLine_GetListByDocumentId(document.DocumentId);
                foreach (var detail in document.Detail)
                {
                    detail.ItemData = Item_GetById((int)detail.DocumentLineItemId);
                }
                return document;
            }
        }
        public List<DocumentLineModel> DocumentLine_GetListByDocumentId(long id)
        {
            using (var _context = new AndromedaContext())
            {
                return _context.DocumentLines.Where(dl => dl.DocumentId == id).ToList();
            }
        }
        public bool DocumentLine_Update(DocumentLineModel line)
        {
            using (var _context = new AndromedaContext())
            {
                if (line.DocumentLineQty <= 0)
                {
                    var toDeleteObject = _context.DocumentLines.Where(l => l.DocumentLineId == line.DocumentLineId && l.DocumentId == line.DocumentId).FirstOrDefault();
                    _context.DocumentLines.Remove(toDeleteObject);
                }
                else
                {
                    var origRecord = _context.DocumentLines.Where(l => l.DocumentLineId == line.DocumentLineId && l.DocumentId == line.DocumentId).FirstOrDefault();
                    origRecord.DocumentLineQty = line.DocumentLineQty;
                    try { origRecord.DocumentLineItemPrice = line.DocumentLineItemPrice; }
                    catch (Exception ex) { }
                }
                _context.SaveChanges();
                return true;
            }
        }
    }
}