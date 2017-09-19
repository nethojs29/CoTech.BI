using CoTech.Bi.Core.EventSourcing.Models;
using CoTech.Bi.Util;

namespace CoTech.Bi.Core.Users.Models
{
    public abstract class UserCommand : Command {}

    public class CreateUserCmd : UserCommand {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public CreateUserCmd(CreateUserReq req, long userId) {
          Name = req.Name;
          Lastname = req.Lastname;
          Email = req.Email.ToLower();
          UserId = userId;
          Password = PasswordGenerator.CreateRandomPassword(8);
        }
    }
}