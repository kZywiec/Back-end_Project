using Core.Entities.DocumentEntities;
using Core.Entities.UserEntities;

namespace Core.Services
{
    public class AuditService
    {

        /// <summary>
        /// Rejestruje operację wykonaną na dokumencie przez użytkownika.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="Document"></param>
        /// <param name="action"></param>
        public void LogDocumentAction(User user, Document Document, string action)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Zwraca historię operacji wykonanych na dokumencie.
        /// </summary>
        /// <param name="Document"></param>
        /// <returns> List<string> DocumentHistory </returns>
        public List<string> GetDocumentHistory(Document Document)
        {
            throw new NotImplementedException();
        }
    }
}
