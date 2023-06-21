using Core.Entities.DocumentEntities;
using Core.Entities.LogEntities;
using Core.Entities.UserEntities;
using Core.Repositories;

namespace Core.Services
{
    public class AuditService
    {
        private readonly LogRepository logRepository;

        public AuditService(LogRepository logRepository)
        {
            this.logRepository = logRepository;
        }

        /// <summary>
        /// Rejestruje operację wykonaną na dokumencie przez użytkownika.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="Document"></param>
        /// <param name="action"></param>
        public void LogDocumentAction(User user, Document document, ActionLog actionLog)
        {
            Log logEntry = new Log(actionLog, user, document);

            logRepository.AddLogAsync(logEntry);
        }


        /// <summary>
        /// Zwraca historię operacji wykonanych na dokumencie.
        /// </summary>
        /// <param name="Document"></param>
        /// <returns> List<Log> DocumentHistory </returns>
        public List<Log> GetDocumentHistory(Document Document)
        {
            List<Log> documentLogs = logRepository.GetLogsByDocumentAsync(Document).Result.ToList();
            return documentLogs;
        }
    }
}
