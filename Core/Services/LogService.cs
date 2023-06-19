using Core.Entities.LogEntities;
using Core.Repositories;

namespace Core.Services
{
    public class LogService
    {
        private readonly LogRepository _logRepository;

        public LogService(LogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task<bool> AddLog(ActionLog Action)
        {
            Log log = new Log();
            log.LogType = ActionLog.Upload;

            await _logRepository.AddLogAsync(log);

            return true;
        }
    }
}
