using Core.Entities.Document;
using Core.Entities.User;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class AuthorizationService
    {
        private readonly UserRepository _userRepository;

        public AuthorizationService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Sprawdza, czy użytkownik ma uprawnienia do dostępu do danego dokumentu.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="document"></param>
        /// <returns></returns>
        public bool IsUserAuthorized(User user, Document document)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Sprawdza, czy użytkownik ma rolę 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool IsAdmin(User user) 
        { 
            throw new NotImplementedException();
        }
    }
}
