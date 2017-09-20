using System;
using System.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Authorization;
using CoTech.Bi.Core.Permissions.Models;
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
      var cmd = new CreateUserCmd(req, HttpContext.UserId().Value);
      var entity = await userRepository.Create(cmd);
      if(entity != null) {
        var sent = MailsHelpers.MailPassword(req.Email, cmd.Password);
        if(sent)
          return Created($"/api/users/{entity.Id}", new UserResponse(entity));
        else
          return StatusCode(500);
      } else {
        return BadRequest();
      }
    }
  }
}