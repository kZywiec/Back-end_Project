using Core.Data;
using Core.Entities.DocumentEntities;
using Core.Entities.UserEntities;

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
        /// <param name="Document"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void AddDocument(Document Document)
        {
            _context.Documents.Add(Document);
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
        /// <param name="DocumentId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Document GetDocumentById(long DocumentId)
        {
            return _context.Documents.Find(DocumentId);
        }


        /// <summary>
        /// Aktualizuje dane dokumentu.
        /// </summary>
        /// <param name="Document"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void UpdateDocument(Document Document)
        {
            var existingDoc = _context.Documents.Find(Document.Id);

            if (existingDoc != null)
            {
                existingDoc.Title = Document.Title;
                existingDoc.FileType = Document.FileType;
                existingDoc.Description = Document.Description;
                existingDoc.CreationDate = Document.CreationDate;
                existingDoc.Uploader = Document.Uploader;
                existingDoc.AccessStatus = Document.AccessStatus;
                existingDoc.FilePath = Document.FilePath;

                _context.SaveChanges();
            }
        }


        /// <summary>
        /// Usuwa dokument z systemu.
        /// </summary>
        /// <param name="Document"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void DeleteDocument(Document Document)
        {
            _context.Documents.Remove(Document);
            _context.SaveChanges();
        }
    }
}
