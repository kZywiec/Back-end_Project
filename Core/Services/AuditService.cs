using Core.Entities.Document;
using Core.Entities.User;

namespace Core.Services
{
    public class AuditService
    {

        /// <summary>
        /// Rejestruje operację wykonaną na dokumencie przez użytkownika.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="document"></param>
        /// <param name="action"></param>
        public void LogDocumentAction(User user, Document document, string action)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Zwraca historię operacji wykonanych na dokumencie.
        /// </summary>
        /// <param name="document"></param>
        /// <returns> List<string> documentHistory </returns>
        public List<string> GetDocumentHistory(Document document)
        {
            throw new NotImplementedException();
        }
    }
}
