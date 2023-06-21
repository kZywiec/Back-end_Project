using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities.UserEntities
{
    public enum UserRole
    {
        [Description("User not registered in system")]
        Guest,

        [Description("User registered in system")]
        User,

        [Description("Administrator")]
        Admin
    }
}
