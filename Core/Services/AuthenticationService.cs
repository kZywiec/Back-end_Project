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
            if (_userRepository.GetUserByUsernameAsync(username) != null)
            {
                throw new Exception("Użytkownik o podanej nazwie już istnieje.");
            }
            
            var hashedPassword = _hashingService.HashPassword(password);

            var user = new User
            {
                Username = username,
                Password = hashedPassword,
                Role = UserRole.User
            };

            _userRepository.AddUserAsync(user);
        }

        /// <summary>
        /// Loguje użytkownika do systemu.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User Login(string username, string password)
        {
            User user = _userRepository.GetUserByUsernameAsync(username).Result;

            if (user == null)
            {
                throw new Exception("Nieprawidłowa nazwa użytkownika lub hasło.");
            }

            
            var isPasswordValid = _hashingService.VerifyPassword(password, user.Password);

            if (!isPasswordValid)
            {
                throw new Exception("Nieprawidłowa nazwa użytkownika lub hasło.");
            }

            var token = _tokenService.GenerateToken(user.Id);

            return user;
            //WithToken
            //{
            //    UserId = user.Id,
            //    Username = user.Username,
            //    Token = token
            //};
        }
    }
}
