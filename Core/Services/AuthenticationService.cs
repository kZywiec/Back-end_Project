using Core.Entities.UserEntities;
using Core.Repositories;

namespace Core.Services
{
    public class AuthenticationService
    {
        private readonly UserRepository _userRepository;
        private readonly HashingService _hashingService;
        private readonly TokenService _tokenService;

        public AuthenticationService(UserRepository userRepository, HashingService hashingService, TokenService tokenService)
        {
            _userRepository = userRepository;
            _hashingService = hashingService;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Rejestruje nowego użytkownika.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        void Register(string username, string password)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Loguje użytkownika do systemu.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        User Login(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
