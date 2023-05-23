using Core.Data;
using Core.Entities.Document;
using Core.Entities.User;
using Microsoft.VisualBasic.FileIO;
using System.Security.Cryptography;

namespace Core.Repositories
{
    public class DocumentRepository
    {
        private readonly ProjectContext _context;

        public DocumentRepository(ProjectContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Dodaje nowy dokument do systemu.
        /// </summary>
        /// <param name="document"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void AddDocument(Document document)
        {
            _context.Documents.Add(document);
            _context.SaveChanges();
        }


        /// <summary>
        /// Zwraca listę publicznych dokumentów.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<Document> GetPublicDocuments()
        {
            return _context.Documents.Where(d => d.AccessStatus == DocumentAccessStatus.Public).ToList();
        }


        /// <summary>
        /// Zwraca listę dokumentów dostępnych dla danego użytkownika.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<Document>  GetAccessibleDocuments(User user)
        {
           return _context.Documents
                .Where(d => d.AccessStatus == DocumentAccessStatus.Public 
                || d.Uploader.Id == user.Id).ToList();
        }


        /// <summary>
        /// Zwraca dokument o określonym identyfikatorze.
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Document GetDocumentById(long documentId)
        {
            return _context.Documents.Find(documentId);
        }


        /// <summary>
        /// Aktualizuje dane dokumentu.
        /// </summary>
        /// <param name="document"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void UpdateDocument(Document document)
        {
            var existingDoc = _context.Documents.Find(document.Id);

            if (existingDoc != null)
            {
                existingDoc.Title = document.Title;
                existingDoc.FileType = document.FileType;
                existingDoc.Description = document.Description;
                existingDoc.CreationDate = document.CreationDate;
                existingDoc.Uploader = document.Uploader;
                existingDoc.AccessStatus = document.AccessStatus;
                existingDoc.FilePath = document.FilePath;

                _context.SaveChanges();
            }
        }


        /// <summary>
        /// Usuwa dokument z systemu.
        /// </summary>
        /// <param name="document"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void DeleteDocument(Document document)
        {
            _context.Documents.Remove(document);
            _context.SaveChanges();
        }
    }
}
