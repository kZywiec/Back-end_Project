﻿using Core.Entities.DocumentEntities;
using Core.Entities.UserEntities;
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
        /// <param name="Document"></param>
        /// <returns></returns>
        public bool IsUserAuthorized(User user, Document Document)
        {
            if(user == null || document == null)
                return false;

            if(IsAdmin(user))
                return true;

            if (Document.AccessStatus.Contains("Public"))
                return true;

            if(Document.AccessStatus.Contains("Private") && user.Role.Contains("User"))
                return true;

            if (Document.AccessStatus.Contains("Confidential") && user.Documents.Contains(Document))
                return true;

            return false;

        }


        /// <summary>
        /// Sprawdza, czy użytkownik ma rolę 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool IsAdmin(User user) 
        { 
            if(user == null) return false;
            return user.Role.Contains("Admin")
        }
    }
}
