using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities.User
{
    public enum UserRole
    {
        [Description("User registered in system")]
        User,

        [Description("Administrator")]
        Admin
    }
}
