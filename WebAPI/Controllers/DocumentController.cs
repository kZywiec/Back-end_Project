using Core.Entities.DocumentEntities;
using Core.Entities.UserEntities;
using Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using Document = Core.Entities.DocumentEntities.Document;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly DocumentRepository _documentRepository;
        private readonly UserRepository _userRepository;

        public DocumentController(DocumentRepository documentRepository, UserRepository userRepository)
        {
            _documentRepository = documentRepository;
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddDocument(string title, string filetype, string description, DocumentAccessStatus accessStatus,string filePath, long userId)
        {
            Document document = new(title,filetype,description,userId,accessStatus, filePath);
            document.Uploader = await _userRepository.GetUserByIdAsync(userId);
            await _documentRepository.AddDocumentAsync(document);
            return Ok();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetPublicDocuments()
        {
            List<Core.Entities.DocumentEntities.Document> publicDocuments = await _documentRepository.GetPublicDocumentsAsync();
            return Ok(publicDocuments);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAccessibleDocuments(long userId)
        {
            User user = await _userRepository.GetUserByIdAsync(userId);
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
