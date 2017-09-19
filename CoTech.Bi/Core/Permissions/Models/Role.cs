using Microsoft.AspNetCore.Identity;

namespace CoTech.Bi.Core.Permissions.Models
{
    public class Role
    {
        public const long Super = 1;
        public const long Admin = 2;
        public const long Reader = 3;
    }
}