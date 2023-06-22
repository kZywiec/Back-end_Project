using Core.Entities.DocumentEntities;
using Core.Entities.LogEntities;
using Core.Entities.UserEntities;
using Core.Repositories;
using System.Reflection.Metadata;
using Document = Core.Entities.DocumentEntities.Document;

namespace Core.Services
{
    public class LogService
    {
        private readonly LogRepository _logRepository;

        public LogService(LogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task<bool> AddLog(ActionLog Action, long userId, long documentId)
        {
            Log log = new Log(Action, userId, documentId);
            await _logRepository.AddLogAsync(log);

            return true;
        }
    }
}
