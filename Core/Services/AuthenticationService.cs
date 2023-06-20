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
        public void Register(string username, string password)
        {
            if (_userRepository.Exists(username))
            {
                throw new Exception("Użytkownik o podanej nazwie już istnieje.");
            }
            
            var hashedPassword = _hashingService.HashPassword(password);
           
            var user = new User
            {
                Username = username,
                Password = hashedPassword,
                Role = UserRole User
            };

            _userRepository.AddUser(user);
        }

        /// <summary>
        /// Loguje użytkownika do systemu.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        User Login(string username, string password)
        {
            var user = _userRepository.GetByUsername(username);

            if (user == null)
            {
                throw new Exception("Nieprawidłowa nazwa użytkownika lub hasło.");
            }

            var isPasswordValid = _hashingService.VerifyPassword(password, user.Password);

            if (!isPasswordValid)
            {
                throw new Exception("Nieprawidłowa nazwa użytkownika lub hasło.");
            }

            var token = _tokenService.GenerateToken(user.UserId);

            return new UserWithToken
            {
                UserId = user.UserId,
                Username = user.Username,
                Token = token
            };
        }
    }
}
