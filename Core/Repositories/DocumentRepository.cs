using Core.Entities.Document;
using Core.Entities.User;
using System.Security.Cryptography;

namespace Core.Repositories
{
    public class DocumentRepository
    {
        /// <summary>
        /// Dodaje nowy dokument do systemu.
        /// </summary>
        /// <param name="document"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void AddDocument(Document document)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Zwraca listę publicznych dokumentów.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<Document> GetPublicDocuments()
        {
            throw new NotImplementedException ();
        }


        /// <summary>
        /// Zwraca listę dokumentów dostępnych dla danego użytkownika.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<Document>  GetAccessibleDocuments(User user)
        {
            throw new NotImplementedException () ;
        }


        /// <summary>
        /// Zwraca dokument o określonym identyfikatorze.
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Document GetDocumentById(long documentId)
        {
            throw new NotImplementedException() ;
        }


        /// <summary>
        /// Aktualizuje dane dokumentu.
        /// </summary>
        /// <param name="document"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void UpdateDocument(Document document)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Usuwa dokument z systemu.
        /// </summary>
        /// <param name="document"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void DeleteDocument(Document document)
        {
            throw new NotImplementedException();
        }
    }
}
