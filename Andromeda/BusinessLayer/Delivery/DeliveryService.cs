using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.BusinessLayer
{
    public partial class Services : IServices
    {
        public DocumentModel Document_GetById(long id)
        {
            return _repository.Document_GetById(id);
        }
        public List<DocumentModel> Document_GetByFilter(DocumentFilter filter)
        {
            return _repository.Document_ByFilter(filter);
        }
        public List<DocumentModel> Document_GetAllByStatus(DocumentStatusEnum status)
        {
            var documents = _repository.Document_GetAllByStatus(DocumentTypeEnum.DELIVERY, status);
            return documents;
        }
        public List<DocumentModel> Document_GetByActivity(int limit)
        {
            List<DocumentModel> responseDocuments = new List<DocumentModel>();
            var documents = _repository.Document_GetAlive();
            var counter = 0;
            foreach (var doc in documents)
            {
                counter++;
                doc.EntityData = Entity_GetById(doc.EntityId);
                responseDocuments.Add(doc);
                if (counter >= limit)
                {
                    break;
                }
            }
            return responseDocuments;
        }
        public ApiResponse Document_Create(DocumentModel document)
        {
            document.DocumentType = DocumentTypeEnum.DELIVERY;
            document.DocumentLastUpdateAt = DateTime.Now;
            try
            {
                var id = _repository.Document_Create(document);
                var number = _repository.Document_GetById(id).DocumentNumber;
                return CustomResponse.Response_Update_Ok(number);
            }
            catch (Exception ex)
            {
                return CustomResponse.Response_Exception("DocumentCreate_", ex);
            }
        }
        public ApiResponse Document_SetPrepared(DocumentModel document)
        {
            DocumentModel origDoc = Document_GetById(document.DocumentId);
            if (origDoc.DocumentStatus != DocumentStatusEnum.GENERATED)
            {
                return CustomResponse.Response_Update_Error();
            }
            else
            {
                document.DocumentStatus = DocumentStatusEnum.PREPARED;
                document.DocumentLastUpdateAt = DateTime.Now;
                document.DocumentPreparedAt = DateTime.Now;
                return Document_UpdateHeaderAndLines(document);
            }
        }
        public ApiResponse Document_SetBilled(DocumentModel document)
        {
            DocumentModel origDoc = Document_GetById(document.DocumentId);
            if (origDoc.DocumentStatus != DocumentStatusEnum.PREPARED)
            {
                return CustomResponse.Response_Update_Error();
            }
            else
            {
                document.DocumentStatus = DocumentStatusEnum.BILLED;
                document.DocumentLastUpdateAt = DateTime.Now;
                document.DocumentBilledAt = DateTime.Now;
                return Document_UpdateHeaderAndLines(document);
            }
        }
        public ApiResponse Document_SetDelivered(DocumentModel document)
        {
            DocumentModel origDoc = Document_GetById(document.DocumentId);
            if (origDoc.DocumentStatus != DocumentStatusEnum.BILLED && origDoc.DocumentStatus != DocumentStatusEnum.DELIVERED_AND_NOT_INCOME)
            {
                return CustomResponse.Response_Update_Error();
            }
            else
            {
                origDoc.DocumentStatus = DocumentStatusEnum.DELIVERED;
                origDoc.DocumentLastUpdateAt = DateTime.Now;
                origDoc.DocumentBilledAt = DateTime.Now;
                origDoc.DocumentTotalAmount = document.DocumentTotalAmount;

                return Document_UpdateHeaderAndLines(origDoc);
            }
        }
        public ApiResponse Document_SetSigned(DocumentModel document)
        {
            DocumentModel origDoc = Document_GetById(document.DocumentId);
            if (origDoc.DocumentStatus != DocumentStatusEnum.BILLED)
            {
                return CustomResponse.Response_Update_Error();
            }
            else
            {
                origDoc.DocumentStatus = DocumentStatusEnum.DELIVERED_AND_NOT_INCOME;
                origDoc.DocumentLastUpdateAt = DateTime.Now;
                origDoc.DocumentBilledAt = DateTime.Now;
                origDoc.DocumentTotalAmount = document.DocumentTotalAmount;

                return Document_UpdateHeaderAndLines(origDoc);
            }
        }
        public ApiResponse Document_SignedDelivered(DocumentModel document)
        {
            DocumentModel origDoc = Document_GetById(document.DocumentId);
            if (origDoc.DocumentStatus != DocumentStatusEnum.DELIVERED_AND_NOT_INCOME)
            {
                return CustomResponse.Response_Update_Error();
            }
            else
            {
                origDoc.DocumentStatus = DocumentStatusEnum.DELIVERED;
                origDoc.DocumentLastUpdateAt = DateTime.Now;
                origDoc.DocumentBilledAt = DateTime.Now;
                origDoc.DocumentTotalAmount = document.DocumentTotalAmount;

                return Document_UpdateHeaderAndLines(document);
            }
        }
        private ApiResponse Document_UpdateHeaderAndLines(DocumentModel document)
        {
            ApiResponse response;
            try
            {
                if (document.DocumentId > 0)
                {
                    foreach (var line in document.Detail)
                    {
                        _repository.DocumentLine_Update(line);
                    }
                    if (_repository.Document_Update(document))
                        response = CustomResponse.Response_Update_Ok((long)document.DocumentId);
                    else
                        response = CustomResponse.Response_Update_Error();
                }
                else
                {
                    response = CustomResponse.Response_NullKey_Error();
                }
            }
            catch (Exception ex)
            {
                response = CustomResponse.Response_Exception("Document_", ex);
            }
            return response;
        }
        public ApiResponse Document_UpdateLine(DocumentLineModel line)
        {
            if (_repository.DocumentLine_Update(line))
                return CustomResponse.Response_Update_Ok((long)line.DocumentLineId);
            else
                return CustomResponse.Response_Update_Error();
        }
        public DocumentModel Document_GetLast()
        {
            return _repository.Document_GetLastByDate(DocumentTypeEnum.DELIVERY);
        }
    }
}