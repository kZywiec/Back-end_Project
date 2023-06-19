using Core.Entities.DocumentEntities;
using Core.Repositories;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Core.Entities.UserEntities
{
    public class User : EntityBase
    {

        public User() : base() { }

        public User(string username, string passworld) : base()
        {
            Username = username;
            Password = passworld;
        }

        public User(string username, string password, UserRole role) : base()
        {
            Username = username;
            Password = password;
            Role = role;
        }


        /// <summary>
        ///  Nazwa użytkownika.
        /// </summary>
        [Required]
        [Display(Name = "Username")]
        [StringLength(20, ErrorMessage = "Username length must be between {2} and {1}.", MinimumLength = 5)]
        public string Username { get; set; }


        /// <summary>
        /// Hasło użytkownika.
        /// </summary>
        [Required]
        [Display(Name = "Password")]
        [StringLength(16, ErrorMessage = "Password length must be between {2} and {1}.", MinimumLength = 5)]
        public string Password { get; set; }


        /// <summary>
        /// Rola użytkownika (np. Admin, User).
        /// </summary>
        [Required]
        [HiddenInput]
        public UserRole Role { get; set; } = UserRole.User;

        /// <summary>
        /// Lista dokumentów
        /// </summary>
        public List<Document> Documents { get; set; }


        /// <summary>
        /// Zwraca listę dokumentów, które użytkownik dodał do systemu.
        /// </summary>
        public List<Document> GetOwnedDocuments()
        {
            return Documents;
        }
    }
}
