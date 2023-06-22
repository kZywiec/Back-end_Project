using Core.Data;
using Core.Entities.DocumentEntities;
using Core.Entities.LogEntities;
using Core.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public class DocumentRepository
    {
        private readonly ProjectContext _context;
        private readonly UserRepository _userRepository;
        private readonly LogRepository _logRepository;

        public DocumentRepository(UserRepository userRepository, LogRepository logRepository, ProjectContext projectContext)
        {
            _userRepository = userRepository;
            _logRepository = logRepository;
            _context = projectContext;
        }

        /// <summary>
        /// Adds a new document to the system.
        /// </summary>
        /// <param name="document">The document to add.</param>
        public async Task AddDocumentAsync(Document document)
        {
            _context.Documents.Add(document);

            Log log = new Log();
            log.Author = document.Uploader;
            log.Document = document;
            log.LogType = ActionLog.Upload;
            await _logRepository.AddLogAsync(log);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Returns a list of public documents.
        /// </summary>
        /// <returns>The list of public documents.</returns>
        public async Task<List<Document>> GetPublicDocumentsAsync()
        {
            return await _context.Documents.Where(d => d.AccessStatus == DocumentAccessStatus.Public).ToListAsync();
        }

        /// <summary>
        /// Returns a list of documents accessible to the given user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The list of accessible documents.</returns>
public async Task<List<Document>> GetAccessibleDocumentsAsync(User user)
{
    if (user.Role == UserRole.Admin)
        return await _context.Documents.ToListAsync();

    return await _context.Documents
        .Where(d => d.AccessStatus == DocumentAccessStatus.Public || d.Uploader.Id == user.Id)
        .ToListAsync();
}


        /// <summary>
        /// Retrieves a document by its ID.
        /// </summary>
        /// <param name="documentId">The ID of the document.</param>
        /// <returns>The retrieved document.</returns>
        public async Task<Document> GetDocumentByIdAsync(long documentId)
        {
            return await _context.Documents.FindAsync(documentId);
        }

        /// <summary>
        /// Updates the data of a document.
        /// </summary>
        /// <param name="document">The document with updated data.</param>
        public async Task UpdateDocumentAsync(Document document)
        {
            var existingDoc = await _context.Documents.FindAsync(document.Id);

            if (existingDoc != null)
            {
                existingDoc.Title = document.Title;
                existingDoc.FileType = document.FileType;
                existingDoc.Description = document.Description;
                existingDoc.CreationDate = document.CreationDate;
                existingDoc.Uploader = document.Uploader;
                existingDoc.AccessStatus = document.AccessStatus;
                existingDoc.FilePath = document.FilePath;

                Log log = new Log();
                log.Author = document.Uploader;
                log.Document = document;
                log.LogType = ActionLog.Edit;
                await _logRepository.AddLogAsync(log);

                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Deletes a document from the system.
        /// </summary>
        /// <param name="document">The document to delete.</param>
        public async Task DeleteDocumentAsync(Document document)
        {
            Log log = new Log();
            log.Author = await _userRepository.GetUserByIdAsync(document.UploaderId);

            log.Document = document;
            log.LogType = ActionLog.Edit;
            await _logRepository.AddLogAsync(log);

            _context.Documents.Remove(document);
            await _context.SaveChangesAsync();
        }
    }
}