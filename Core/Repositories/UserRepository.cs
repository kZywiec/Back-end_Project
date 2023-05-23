using Core.Data;
using Core.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        /// Pobiera użytkownika na podstawie jego identyfikatora.
        /// </summary>
        /// <param name="userId">Identyfikator użytkownika.</param>
        /// <returns>Użytkownik.</returns>
        public User GetUserById(long userId)
        {
            return _context.Users.Find(userId);
        }

        /// <summary>
        /// Pobiera użytkownika na podstawie jego nazwy użytkownika.
        /// </summary>
        /// <param name="username">Nazwa użytkownika.</param>
        /// <returns>Użytkownik.</returns>
        public User GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }

        /// <summary>
        /// Dodaje nowego użytkownika do systemu.
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void AddUser(User user) 
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        /// <summary>
        /// Zapisuje użytkownika w repozytorium.
        /// </summary>
        /// <param name="user">Użytkownik do zapisania.</param>
        public void SaveUser(User user)
        {
            User existingUser = _context.Users.Find(user.Id);

            if(existingUser != null) 
            {
                existingUser.Username = user.Username; 
                existingUser.Password = user.Password;
                existingUser.Role = user.Role;
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Usuwa użytkownika z repozytorium.
        /// </summary>
        /// <param name="user">Użytkownik do usunięcia.</param>
        public void DeleteUser(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}
