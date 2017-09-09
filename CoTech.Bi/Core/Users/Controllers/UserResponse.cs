using CoTech.Bi.Core.Users.Models;

namespace CoTech.Bi.Core.Users.Controllers
{
    public class UserResponse
    {
        public interface IUserResponse {}
        public class UserNoPassRes : IUserResponse
        {
            public long Id { get; set; }
            public string Name { get; set; }
            public string Lastname { get; set; }
            public string Email { get; set; }

            public UserNoPassRes(long Id,string Name,string Lastname,string Email)
            {
                this.Id = Id;
                this.Name = Name;
                this.Lastname = Lastname;
                this.Email = Email;
            }
        }
    }
}