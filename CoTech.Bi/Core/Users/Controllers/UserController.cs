using System;
using System.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Authorization;
using CoTech.Bi.Core.Permissions.Model;
using CoTech.Bi.Core.Users.Models;
using CoTech.Bi.Core.Users.Repositories;
using CoTech.Bi.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoTech.Bi.Core.Users.Controllers {
  [Route("api/users")]
    public class UserController : Controller
  {
    private readonly UserRepository userRepository;

    public UserController(UserRepository userRepository)
    {
      this.userRepository = userRepository;
    }

    [HttpGet]
    [RequiresAbsoluteRoleAnywhere(Role.Super, Role.Admin)]
    public async Task<IActionResult> GetAll() {
      var users = await userRepository.GetAll();
      return new OkObjectResult(users.Select(u => new UserResponse(u)));
    }

    [HttpPost]
    [RequiresRoot]
    public async Task<IActionResult> Create([FromBody] CreateUserReq req){
      var password = PasswordGenerator.CreateRandomPassword(8);
      var entity = req.toEntity();
      var result = await userRepository.Create(entity, password);
      if(result.Succeeded) {
        return new CreatedResult($"/api/users/{entity.Id}", new UserResponse(entity));
      } else {
        return new BadRequestObjectResult(result.Errors);
      }
    }
  }
}