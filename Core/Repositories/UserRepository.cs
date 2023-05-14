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
        /// <summary>
        /// Pobiera użytkownika na podstawie jego identyfikatora.
        /// </summary>
        /// <param name="userId">Identyfikator użytkownika.</param>
        /// <returns>Użytkownik.</returns>
        public User GetUserById(long userId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Pobiera użytkownika na podstawie jego nazwy użytkownika.
        /// </summary>
        /// <param name="username">Nazwa użytkownika.</param>
        /// <returns>Użytkownik.</returns>
        public User GetUserByUsername(string username)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Dodaje nowego użytkownika do systemu.
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void AddUser(User user) 
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Zapisuje użytkownika w repozytorium.
        /// </summary>
        /// <param name="user">Użytkownik do zapisania.</param>
        public void SaveUser(User user)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Usuwa użytkownika z repozytorium.
        /// </summary>
        /// <param name="user">Użytkownik do usunięcia.</param>
        public void DeleteUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
