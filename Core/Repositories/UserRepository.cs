using Core.Data;
using Core.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public class UserRepository
    {
        private readonly ProjectContext _context;

        public UserRepository(ProjectContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>The retrieved user.</returns>
        public async Task<User> GetUserByIdAsync(long userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        /// <summary>
        /// Retrieves a user by their username.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <returns>The retrieved user.</returns>
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        /// <summary>
        /// Retrieves all users from the repository.
        /// </summary>
        /// <returns>A list of all users.</returns>
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        /// <summary>
        /// Adds a new user to the system.
        /// </summary>
        /// <param name="user">The user to add.</param>
        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates the user in the repository.
        /// </summary>
        /// <param name="user">The user to update.</param>
        public async Task UpdateUserAsync(User user)
        {
            User existingUser = await _context.Users.FindAsync(user.Id);

            if (existingUser != null)
            {
                existingUser.Username = user.Username;
                existingUser.Password = user.Password;
                existingUser.Role = user.Role;

                await _context.SaveChangesAsync();
            }
        }


        /// <summary>
        /// Deletes a user from the repository.
        /// </summary>
        /// <param name="user">The user to delete.</param>
        public async Task DeleteUserAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
