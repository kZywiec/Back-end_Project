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
        /// <param name="userId"></param>
        /// <param name="documentId"></param>
        /// <param name="action"></param>
        public void LogDocumentAction(long userId, long documentId, ActionLog actionLog)
        {
            Log logEntry = new Log(actionLog, userId, documentId);

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
