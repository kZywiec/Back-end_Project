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
    public class DocumentsController : ControllerBase
    {
        private readonly DocumentRepository _documentRepository;
        private readonly UserRepository _userRepository;

        public DocumentsController(DocumentRepository documentRepository, UserRepository userRepository)
        {
            _documentRepository = documentRepository;
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddDocument(string title, string filetype, string description, DocumentAccessStatus accessStatus, long userId)
        {
            Document document = new(title,filetype,description,userId,accessStatus);
            document.Uploader = await _userRepository.GetUserByIdAsync(userId);
            await _documentRepository.AddDocumentAsync(document);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDocuments()
        {
            List<Document> Documents = await _documentRepository.GetAllDocumentsAsync();
            return Ok(Documents);
        }

        // Tutaj pojawia się pbroblem z powodu braku indywidualnej ściażki.
        // Wrazie potrzebu zdefinuje się ścieżkę. Albo można całość przerobić na wyszukiwanie po dostępności

        //[HttpGet]
        //[Route("[action]")]
        //public async Task<IActionResult> GetPublicDocuments()
        //{
        //    List<Core.Entities.DocumentEntities.Document> publicDocuments = await _documentRepository.GetPublicDocumentsAsync();
        //    return Ok(publicDocuments);
        //}

        [HttpGet]
        [Route("{userId?}")]
        public async Task<IActionResult> GetAccessibleDocuments(long userId)
        {
            User user = await _userRepository.GetUserByIdAsync(userId);
            List<Document> accessibleDocuments = await _documentRepository.GetAccessibleDocumentsAsync(user);
            return Ok(accessibleDocuments);
        }

        [HttpGet]
        [Route("{id?}")]
        public async Task<IActionResult> GetDocumentById(long documentId)
        {
            Document document = await _documentRepository.GetDocumentByIdAsync(documentId);
            if (document != null)
            {
                return Ok(document);
            }
            return NotFound();
        }

        [HttpPatch]
        [Route("{id?}")]
        public async Task<IActionResult> ChangeDocumentAccess(long documentId, DocumentAccessStatus documentAccessStatus)
        {
            Document existingDocument = await _documentRepository.GetDocumentByIdAsync(documentId);
            if (existingDocument == null)
            {
                return NotFound();
            }
            existingDocument.AccessStatus = documentAccessStatus;
            await _documentRepository.UpdateDocumentAsync(existingDocument);
            return Ok();
        }

        [HttpPut]
        [Route("{id?}")]
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
        [Route("{id?}")]
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
