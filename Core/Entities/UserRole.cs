using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public enum UserRole
    {
        [Description("User registered in system")]
        User,

        [Description("Administrator")]
        Admin
    }
}
