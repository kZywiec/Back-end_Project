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
        private readonly TokenService _tokenService;

        public AuthenticationController(AuthenticationService authenticationService, TokenService tokenService)
        {
            _authenticationService = authenticationService;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Register(string username, string password)
        {
            _authenticationService.Register(username, password);
            return Ok();
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Login(string username, string password)
        {
            User user = _authenticationService.Login(username, password);
            if (user != null)
            {
                // Generate and return authentication token
                string token = _tokenService.GenerateToken(user.Id);
                return Ok(new { Token = token });
            }

            return Unauthorized();
        }
    }
}
