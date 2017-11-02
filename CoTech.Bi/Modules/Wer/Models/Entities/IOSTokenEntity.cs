using CoTech.Bi.Core.Users.Models;

namespace CoTech.Bi.Modules.Wer.Models.Entities
{
    public class IOSTokenEntity
    {
        public long Id { set; get; }
        public long UserId { set; get; }
        public UserEntity User { set; get; }
        public string Token { set; get; }
    }
}