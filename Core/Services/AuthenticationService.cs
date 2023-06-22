using Core.Entities.UserEntities;
using Core.Repositories;

namespace Core.Services
{
    public class AuthenticationService
    {
        private readonly UserRepository _userRepository;
        private readonly HashingService _hashingService;
        private User? LoggedUser;

        public AuthenticationService(UserRepository userRepository, HashingService hashingService)
        {
            _userRepository = userRepository;
            _hashingService = hashingService;
        }

        /// <summary>
        /// Rejestruje nowego użytkownika.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public async Task RegisterAsync(string username, string password)
        {
            if (await _userRepository.GetUserByUsernameAsync(username) != null)
                throw new Exception("Użytkownik o podanej nazwie już istnieje.");
          
            //var hashedPassword = _hashingService.HashPassword(password);

            var user = new User
            {
                Username = username,
                Password = password, //hashedPassword, 
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
        public void Login(string username, string password)
        {
            User user = _userRepository.GetUserByUsernameAsync(username).Result;

            if (user == null)
                throw new Exception("Nieprawidłowa nazwa użytkownika lub hasło.");

            //if (!_hashingService.VerifyPassword(password, user.Password))
            if(password != user.Password)  
                throw new Exception("Nieprawidłowa nazwa użytkownika lub hasło.");
            LoggedUser = user;
        }

        public void Logout()
            => LoggedUser = null;

        public bool IsUserLogged()
            => LoggedUser != null;
    }
}
