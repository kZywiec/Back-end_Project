using Core.Entities.UserEntities;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationService _authenticationService;

        public AuthenticationController(AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register(string username, string password)
        {
            await _authenticationService.RegisterAsync(username, password);
            return Ok();
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Login(string username, string password)
        {
            _authenticationService.Login(username, password);
            return _authenticationService.IsUserLogged() ? Ok() : Unauthorized();
        }
    }
}
