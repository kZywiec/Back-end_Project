using Core.Entities.UserEntities;
using Core.Entities.DocumentEntities;

namespace Core.Entities.LogEntities
{
    public class Log : EntityBase
    {
        public Log(ActionLog logType, long authorId, long documentId) : base()
        {
            LogType = logType;
            AuthorId = authorId;
            DocumentId = documentId;
        }

        public ActionLog LogType { get; set; }
        public long AuthorId { get; set; }
        public User Author { get; set; }

        public long DocumentId { get; set; }
        public Document Document { get; set; }
    }
}