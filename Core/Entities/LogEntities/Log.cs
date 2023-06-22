using Core.Entities.UserEntities;
using Core.Entities.DocumentEntities;

namespace Core.Entities.LogEntities
{
    public class Log : EntityBase
    {
        public Log() : base() { }
        public Log(ActionLog logType, User author, Document document) : base()
        {
            LogType = logType;
            Author = author;
            Document = document;
        }

        public ActionLog LogType { get; set; }
        public long AuthorId { get; set; }
        public User Author { get; set; }

        public long DocumentId { get; set; }
        public Document Document { get; set; }
    }
}