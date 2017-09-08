using System.Threading.Tasks;
using CoTech.Bi.Core.Users.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoTech.Bi.Core.Users
{
    [Route("/api/me")]
    public class MeController : Controller
    {
        private UserRepository userRepository;
        public MeController(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        // [HttpGet]
        // public async Task<IActionResult> GetMyself() {
          
        // }
    }
}