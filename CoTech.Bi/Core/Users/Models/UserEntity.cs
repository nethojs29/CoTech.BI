using Microsoft.AspNetCore.Identity;

namespace CoTech.Bi.Core.Users.Models
{
    public class UserEntity : IdentityUser<long>
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
    }
}