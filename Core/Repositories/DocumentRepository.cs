using Core.Data;
using Core.Entities.DocumentEntities;
using Core.Entities.LogEntities;
using Core.Entities.UserEntities;

namespace Core.Repositories
{
    public class DocumentRepository
    {
        private readonly ProjectContext _context;
        private readonly LogRepository _logRepository;

        public DocumentRepository(ProjectContext context, LogRepository logRepository)
        {
            _context = context;
            _logRepository = logRepository;
        }


        /// <summary>
        /// Dodaje nowy dokument do systemu.
        /// </summary>
        /// <param name="Document"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void AddDocument(Document Document)
        {
            _context.Documents.Add(Document);

            Log log = new Log();
            log.Author = Document.Uploader;
            log.Document = Document;
            log.LogType = ActionLog.Upload;
            _logRepository.AddLogAsync(log).GetAwaiter().GetResult();

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
                || d.Uploader.Id == user.Id || user.Role == UserRole.Admin).ToList();
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

                Log log = new Log();
                log.Author = Document.Uploader;
                log.Document = Document;
                log.LogType = ActionLog.Edit;
                _logRepository.AddLogAsync(log).GetAwaiter().GetResult();

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
            Log log = new Log();
            log.Author = Document.Uploader;
            log.Document = Document;
            log.LogType = ActionLog.Edit;
            _logRepository.AddLogAsync(log).GetAwaiter().GetResult();

            _context.Documents.Remove(Document);
            _context.SaveChanges();
        }
    }
}
