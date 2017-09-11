using System;
using System.Threading.Tasks;
using CoTech.Bi.Authorization;
using CoTech.Bi.Core.Permissions.Model;
using CoTech.Bi.Core.Users.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoTech.Bi.Core.Users.Controllers {
  [Route("api/users")]
    public class UserController : Controller
  {
    private readonly UserManager<UserEntity> userManager;
    private readonly SignInManager<UserEntity> signInManager;
    private readonly UserRepository userRepository;

    public UserController(UserManager<UserEntity> userManager, 
                          SignInManager<UserEntity> signInManager,
                          UserRepository userRepository)
    {
      this.userManager = userManager;
      this.signInManager = signInManager;
      this.userRepository = userRepository;
    }

    [HttpGet]
    [RequiresRoot]
    public async Task<IActionResult> GetAll() {
      return new OkObjectResult(userRepository.GetAll());
    }

    [HttpPost]
    [RequiresRoot]
    public async Task<IActionResult> Create([FromBody] CreateUserReq req){
      var password = CreateRandomPassword(8);
      var entity = req.toEntity();
      Console.WriteLine($"{req.Name}, {password}");
      var result = await userManager.CreateAsync(entity, password);
      if(result.Succeeded) {
        return new CreatedResult($"/api/users/{entity.Id}", entity);
      } else {
        return new BadRequestObjectResult(result.Errors);
      }
    }

    public static string CreateRandomPassword(int passwordLength)
    {
      string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
      char[] chars = new char[passwordLength];
      Random rd = new Random();

      for (int i = 0; i < passwordLength; i++)
      {
        chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
      }

      return new string(chars);
    }

  }
}