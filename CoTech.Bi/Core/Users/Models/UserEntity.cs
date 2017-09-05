using Microsoft.AspNetCore.Identity;

namespace CoTech.Bi.Core.Users.Models
{
    public class UserEntity
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}