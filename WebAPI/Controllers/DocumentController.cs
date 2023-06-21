using Core.Entities.DocumentEntities;
using Core.Entities.UserEntities;
using Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly DocumentRepository _documentRepository;

        public DocumentController(DocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddDocument(Document document)
        {
            await _documentRepository.AddDocumentAsync(document);
            return Ok();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetPublicDocuments()
        {
            List<Document> publicDocuments = await _documentRepository.GetPublicDocumentsAsync();
            return Ok(publicDocuments);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAccessibleDocuments(User user)
        {
            List<Document> accessibleDocuments = await _documentRepository.GetAccessibleDocumentsAsync(user);
            return Ok(accessibleDocuments);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetDocumentById(long documentId)
        {
            Document document = await _documentRepository.GetDocumentByIdAsync(documentId);
            if (document != null)
            {
                return Ok(document);
            }
            return NotFound();
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateDocument(long documentId, Document document)
        {
            Document existingDocument = await _documentRepository.GetDocumentByIdAsync(documentId);
            if (existingDocument == null)
            {
                return NotFound();
            }

            existingDocument.Title = document.Title;
            existingDocument.FileType = document.FileType;
            existingDocument.Description = document.Description;
            existingDocument.CreationDate = document.CreationDate;
            existingDocument.Uploader = document.Uploader;
            existingDocument.AccessStatus = document.AccessStatus;
            existingDocument.FilePath = document.FilePath;

            await _documentRepository.UpdateDocumentAsync(existingDocument);
            return Ok();
        }

        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> DeleteDocument(long documentId)
        {
            Document document = await _documentRepository.GetDocumentByIdAsync(documentId);
            if (document == null)
            {
                return NotFound();
            }

            await _documentRepository.DeleteDocumentAsync(document);
            return Ok();
        }
    }
}
