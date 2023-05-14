using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class TokenService
    {
        private readonly string _secretKey;
        private readonly TimeSpan _expirationTime;

        public TokenService(string secretKey, TimeSpan expirationTime)
        {
            _secretKey = secretKey;
            _expirationTime = expirationTime;
        }

        /// <summary>
        /// Generuje token JWT (JSON Web Token) 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string GenerateToken(long userId)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Weryfikacuje token JWT (JSON Web Token)
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool ValidateToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}
