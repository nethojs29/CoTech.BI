using System;
using System.Threading.Tasks;
using CoTech.Bi.Authorization;
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
    [RequiresPermission("Root")]
    public async Task<IActionResult> GetAll() {
      return new OkObjectResult(await userRepository.GetAll());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserReq req){
      var password = new Random().NextDouble().ToString();
      var entity = req.toEntity();
      var result = await userManager.CreateAsync(entity, password);
      if(result.Succeeded) {
        return new CreatedResult($"/api/users/{entity.Id}", entity);
      } else {
        return new BadRequestObjectResult(result.Errors);
      }
    }

    // public async Task<IActionResult> LogIn([FromBody] LogInReq req){
    //   var result = await signInManager.PasswordSignInAsync(req.Email, req.Password, false, false);
    //   if(result.Succeeded){
    //     return new OkObjectResult(userManager.Toke)
    //   }
    // }
  }
}