using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class HashingService
    {

        /// <summary>
        /// Haszuje hasło by bezpiecznie przechowywać je w bazie danych.
        /// </summary>
        /// <param name="password">Hasło podawane przy Rejestracji</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string HashPassword(string password)
        {
            // TODO: Implementacja funkcji haszującej hasło

            throw new NotImplementedException();
        }


        /// <summary>
        /// Weryfikuje, czy podane hasło zgadza się z wcześniej zahaszowanym hasłem.
        /// </summary>
        /// <param name="password">Hasło podawane przy logowaniu</param>
        /// <param name="hashedPassword">Hasło zapisywane w bazie</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool VerifyPassword(string password, string hashedPassword)
        {
            // TODO: Implementacja funkcji weryfikującej hasło

            throw new NotImplementedException();
        }
    }
}
