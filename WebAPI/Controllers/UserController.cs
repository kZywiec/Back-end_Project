using Core.Entities.UserEntities;
using Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddUser(User user)
        {
            await _userRepository.AddUserAsync(user);
            return Ok();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllUsers()
        {
            List<User> users = await _userRepository.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetUserById(long userId)
        {
            User user = await _userRepository.GetUserByIdAsync(userId);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateUser(long userId, [FromBody] User user)
        {
            try
            {
                if (user == null || userId != user.Id)
                {
                    return BadRequest();
                }

                await _userRepository.UpdateUserAsync(user);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating user: {ex.Message}");
            }
        }


        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> DeleteUser(long userId)
        {
            User user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            await _userRepository.DeleteUserAsync(user);
            return Ok();
        }
    }
}
