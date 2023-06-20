using Core.Entities.DocumentEntities;
using Core.Entities.LogEntities;
using Core.Entities.UserEntities;
using Core.Repositories;

namespace Core.Services
{
    public class AuditService
    {
        LogRepository logRepository;
        /// <summary>
        /// Rejestruje operację wykonaną na dokumencie przez użytkownika.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="Document"></param>
        /// <param name="action"></param>
        public void LogDocumentAction(User user, Document Document, string action)
        {
            var actionLog = ActionLog(action);
            var logEntry = new Log(actionLog, user, document);

            logRepository.AddLogAsync(logEntry);
        }


        /// <summary>
        /// Zwraca historię operacji wykonanych na dokumencie.
        /// </summary>
        /// <param name="Document"></param>
        /// <returns> List<string> DocumentHistory </returns>
        public List<string> GetDocumentHistory(Document Document)
        {
            var documentLogs = logRepository.GetLogsByDocumentAsync(Document);

            return documentLogs;
        }
    }
}
