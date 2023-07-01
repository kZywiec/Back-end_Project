using Core.Entities.UserEntities;
using Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        public UsersController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        //[HttpPost]
        //[Route("[action]")]
        //public async Task<IActionResult> AddUser(User user)
        //{
        //    await _userRepository.AddUserAsync(user);
        //    return Ok();
        //}

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            List<User> users = await _userRepository.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet]
        [Route("{id?}")]
        public async Task<IActionResult> GetUserById(long userId)
        {
            User user = await _userRepository.GetUserByIdAsync(userId);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }

        [HttpPatch]
        [Route("{id?}")]
        public async Task<IActionResult> ChangeUserRole(long userId, UserRole userRole)
        {
            try
            {
                User user = await _userRepository.GetUserByIdAsync(userId);
                if (user == null)
                {
                    return NotFound();
                }

                user.Role = userRole;
                await _userRepository.UpdateUserAsync(user);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating user: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("{id?}")]
        public async Task<IActionResult> UpdateUser(long userId, [FromBody] User user)
        {
            try
            {
                if (user == null || userId != user.Id)
                {
                    return BadRequest();
                }

                await _userRepository.UpdateUserAsync(user);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating user: {ex.Message}");
            }
        }


        [HttpDelete]
        [Route("{id?}")]
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
