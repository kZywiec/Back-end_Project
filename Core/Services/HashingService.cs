using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
            using (var sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);

                string result = "";

                foreach(byte b in hashBytes)
                {
                    result += b.ToString("X2");
                }

                return result;  
            }
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
            string hashedToVerify = HashPassword(password);
            return hashedToVerify == hashedPassword;
        }
    }
}
