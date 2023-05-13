using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Core.Entities
{
    public class User : EntityBase
    {
        public User(string login, string passworld): base() 
        { 
            this.Login = login;
            this.Password = passworld;
        }

        [Required]
        [Display(Name = "Login")]
        [StringLength(20, ErrorMessage = "Login length must be between {2} and {1}.", MinimumLength = 5)]
        public string Login { get; set; }

        [Required]
        [Display(Name = "Password")]
        [StringLength(16, ErrorMessage = "Password length must be between {2} and {1}.", MinimumLength = 5)]
        public string Password { get; set; }

        [Required]
        [HiddenInput]
        public UserRole Role { get; set; } = UserRole.User;



        //public void ChangePassword(string oldPassword, string newPassword)
        //    => this.Password = newPassword;

        //public void SetAdmin()
        //    => this.Role = UserRole.Admin;

        //public void RemoveAdmin()
        //    => this.Role = UserRole.User;

        //public bool IsAdmin()
        //    => this.Role == UserRole.Admin;
    }
}
