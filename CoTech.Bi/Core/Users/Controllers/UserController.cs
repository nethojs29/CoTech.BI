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
    private readonly IPasswordHasher<UserEntity> passwordHasher;

    public UserController(UserRepository userRepository, IPasswordHasher<UserEntity> passwordHasher)
    {
      this.userRepository = userRepository;
      this.passwordHasher = passwordHasher;
    }

    [HttpGet]
    [RequiresAuth]
    public async Task<IActionResult> GetAll() {
      var users = await userRepository.GetAll();
      return new OkObjectResult(users.Select(u => new UserResponse(u)));
    }

    [HttpGet("{user}")]
    [RequiresAbsoluteRoleAnywhere(Role.Super, Role.Admin)]
      public async Task<IActionResult> GetUserInCompany(long company, long user) {
          var userEntity = await userRepository.WithId(user);
          return Ok(userEntity);
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
    
    [HttpPost("password")]
    [RequiresRoot]
    public async Task<IActionResult> CreateWithPassword([FromBody] CreateUserPasswordReq req){
      var cmd = new CreateUserCmd(req, HttpContext.UserId().Value);
      var entity = await userRepository.Create(cmd);
      if(entity != null) {
        var sent = MailsHelpers.MailPassword("lmoya@cotecnologias.com", cmd.Password);
        if(sent)
          return Created($"/api/users/{entity.Id}", new UserResponse(entity));
        else
          return StatusCode(500);
      } else {
        return BadRequest();
      }
    }

    [HttpPut("{id}")]
    [RequiresAuth]
    public async Task<IActionResult> Update(long id, [FromBody] UpdateUserReq req) {
      var cmd = new UpdateUserCmd(req, id, HttpContext.UserId().Value);
      var updated = await userRepository.Update(cmd);
      var user = await userRepository.WithId(id);
      if (updated) {
        return Ok(user);
      } else {
        return BadRequest();
      }
    }

    [HttpPut("{id}/password")]
    public async Task<IActionResult> ChangePassword(long id, [FromBody] ChangePasswordReq req) {
      var user = await userRepository.WithId(id);
      var result = passwordHasher.VerifyHashedPassword(user, user.Password, req.OldPassword);
      if (result == PasswordVerificationResult.Failed) {
        return BadRequest();
      }
      var hashedPass = passwordHasher.HashPassword(user, req.NewPassword);
      var cmd = new ChangePasswordCmd(hashedPass, id);
      var changed = await userRepository.ChangePassword(cmd);
      if (changed) return Ok();
      return BadRequest();
    }
  }
}