using Core.Entities.LogEntities;
using Core.Entities.UserEntities;
using Core.Entities.DocumentEntities;
using Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogsController : ControllerBase
    {
        private readonly LogRepository _logRepository;
        private readonly UserRepository _userRepository;
        private readonly DocumentRepository _documentRepository;

        public LogsController(LogRepository logRepository, UserRepository userRepository, DocumentRepository documentRepository)
        {
            _logRepository = logRepository;
            _userRepository = userRepository;
            _documentRepository = documentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLogs()
            =>Ok(await _logRepository.GetAllAsync());

        [HttpGet]
        [Route("{id?}")]
        public async Task<IActionResult> GetLogById(long id)
        {
            try
            {
                // Pobierz log o podanym ID z repozytorium logów
                Log log = await _logRepository.GetLogByIdAsync(id);
                return Ok(log);
            }
            catch (FileNotFoundException)
            {
                // Jeśli log nie został znaleziony, zwróć odpowiedź "not found"
                return NotFound();
            }
            catch (Exception ex)
            {
                // Jeśli wystąpił wyjątek, zwróć odpowiedź "bad request" wraz z wiadomością z wyjątku
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{userId?}")]
        public async Task<IActionResult> GetLogByUserId(long authorId)
        {
            // Pobierz użytkownika o podanym ID z repozytorium użytkowników
            User author = await _userRepository.GetUserByIdAsync(authorId);
            // Pobierz logi według autora z repozytorium logów
            var logs = await _logRepository.GetLogsByAuthorAsync(author);
            return Ok(logs);
        }

        [HttpGet]
        [Route("{documentId?}")]
        public async Task<IActionResult> GetLogByDocumentId(long documentId)
        {
            // Pobierz dokument o podanym ID z repozytorium dokumentów
            Document document = await _documentRepository.GetDocumentByIdAsync(documentId);
            // Pobierz logi według dokumentu z repozytorium logów
            var logs = await _logRepository.GetLogsByDocumentAsync(document);
            return Ok(logs);
        }

        //[HttpPost]
        //[Route("[action]")]
        //public async Task<IActionResult> AddLog(Log log)
        //{
        //    try
        //    {
        //        // Dodaj log do repozytorium logów
        //        await _logRepository.AddLogAsync(log);
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        // Jeśli wystąpił wyjątek, zwróć odpowiedź "bad request" wraz z wiadomością z wyjątku
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpDelete]
        [Route("{id?}")]
        public async Task<IActionResult> DeleteLog(long id)
        {
            try
            {
                // Pobierz log o podanym ID z repozytorium logów
                Log log = await _logRepository.GetLogByIdAsync(id);
                // Usuń log z repozytorium logów
                await _logRepository.RemoveLogAsync(log);
                return Ok();
            }
            catch (FileNotFoundException)
            {
                // Jeśli log nie został znaleziony, zwróć odpowiedź "not found"
                return NotFound();
            }
            catch (Exception ex)
            {
                // Jeśli wystąpił wyjątek, zwróć odpowiedź "bad request" wraz z wiadomością z wyjątku
                return BadRequest(ex.Message);
            }
        }
    }
}