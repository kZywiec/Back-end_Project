using Core.Entities.LogEntities;
using Core.Entities.UserEntities;
using Core.Entities.DocumentEntities;
using System.Linq;
using System.Threading.Tasks;
using Core.Data;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories
{
    public class LogRepository
    {
        private readonly ProjectContext _context;

        public LogRepository(ProjectContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Pobiera wszystkie logi.
        /// </summary>
        /// <returns>List<Log></returns>
        public async Task<List<Log>> GetAllAsync()
        {
            var logs = await _context.Logs.ToListAsync();
            if (!logs.Any())
                throw new Exception ("Nie znalezione logów");
            return await _context.Logs.ToListAsync();
        }

        /// <summary>
        /// Pobiera log na podstawie jego identyfikatora.
        /// </summary>
        /// <param name="logId">Identyfikator logu.</param>
        /// <returns>Log.</returns>
        public async Task<Log> GetLogByIdAsync(long logId)
        {
            if (logId <= 0)
                throw new Exception("There is problem with Id, is less the 0.");

            Log? _Log = await _context.Logs.FindAsync(logId);
            
            if (_Log != null)
                return _Log;
            else
            throw new FileNotFoundException();
            
            
        }

        /// <summary>
        /// Pobiera logi na podstawie autora.
        /// </summary>
        /// <param name="author">Autor logów.</param>
        /// <returns>Lista logów.</returns>
        public async Task<IEnumerable<Log>> GetLogsByAuthorAsync(User author)
        {
            if (author == null)
                return Enumerable.Empty<Log>();

            return await Task.FromResult(_context.Logs.Where(log => log.Author == author).ToList());
        }

        /// <summary>
        /// Pobiera logi na podstawie dokumentu.
        /// </summary>
        /// <param name="document">Dokument logów.</param>
        /// <returns>Lista logów.</returns>
        public async Task<IEnumerable<Log>> GetLogsByDocumentAsync(Document document)
        {
            if (document == null)
                return Enumerable.Empty<Log>();

            return await Task.FromResult(_context.Logs.Where(log => log.Document == document).ToList());
        }

        /// <summary>
        /// Dodaje nowy log do repozytorium.
        /// </summary>
        /// <param name="log">Log do dodania.</param>
        /// <returns>Task.</returns>
        public async Task AddLogAsync(Log log)
        {
            if (log == null)
                return;

            _context.Logs.Add(log);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Usuwa log z repozytorium.
        /// </summary>
        /// <param name="log">Log do usunięcia.</param>
        /// <returns>Task.</returns>
        public async Task RemoveLogAsync(Log log)
        {
            if (log == null)
                return;

            _context.Logs.Remove(log);
            await _context.SaveChangesAsync();
        }
    }
}