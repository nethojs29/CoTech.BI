using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Net;
using CoTech.Bi.Core.Users.Models;
using CoTech.Bi.Core.Permissions.Model;

namespace CoTech.Bi.Core.Users.Controllers
{
	[Route("api/auth")]
	public class AuthController : Controller
    {
		private readonly UserManager<UserEntity> _userManager;
		private readonly SignInManager<UserEntity> _signInManager;
		private readonly RoleManager<Role> _roleManager;
		private IPasswordHasher<UserEntity> _passwordHasher;
		private IConfiguration _configurationRoot;
		private ILogger<AuthController> _logger;


		public AuthController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, RoleManager<Role> roleManager
			, IPasswordHasher<UserEntity> passwordHasher, IConfiguration configurationRoot, ILogger<AuthController> logger)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleManager;
			_logger = logger;
			_passwordHasher = passwordHasher;
			_configurationRoot = configurationRoot;
		}

		[HttpPost("CreateToken")]
		[Route("token")]
		public async Task<IActionResult> CreateToken([FromBody] LogInReq model)
		{
			try
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				if(user == null)
				{
					return Unauthorized();
				}
				if (_passwordHasher.VerifyHashedPassword(user, user.Password, model.Password) == PasswordVerificationResult.Success)
				{
					var userClaims = await _userManager.GetClaimsAsync(user);

					var claims = new[]
					{
						new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
						new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
						new Claim(JwtRegisteredClaimNames.Email, user.Email)
					}.Union(userClaims);

					var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configurationRoot["JwtSecurityToken:Key"]));
					var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

					var jwtSecurityToken = new JwtSecurityToken(
						issuer: _configurationRoot["JwtSecurityToken:Issuer"],
						audience: _configurationRoot["JwtSecurityToken:Audience"],
						claims: claims,
						expires: DateTime.UtcNow.AddMinutes(60),
						signingCredentials: signingCredentials
						);
					return Ok(new 
					{
						token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
						expiration = jwtSecurityToken.ValidTo
					});
				}
				return Unauthorized();
			}
			catch (Exception ex)
			{
				_logger.LogError($"error while creating token: {ex}");
				return StatusCode((int)HttpStatusCode.InternalServerError, "error while creating token");
			}
		}
	}
}