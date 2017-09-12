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
using CoTech.Bi.Util;
using CoTech.Bi.Core.Users.Repositories;
using Newtonsoft.Json;

namespace CoTech.Bi.Core.Users.Controllers
{
	[Route("api/auth")]
	public class AuthController : Controller
    {
		private readonly UserRepository _userRepository;
		private IPasswordHasher<UserEntity> _passwordHasher;
		private JwtTokenGenerator _jwtTokenGenerator;
		private ILogger<AuthController> _logger;

		public AuthController(UserRepository userRepository, 
													IPasswordHasher<UserEntity> passwordHasher, 
													JwtTokenGenerator jwtTokenGenerator, 
													ILogger<AuthController> logger)
		{
			_userRepository = userRepository;
			_logger = logger;
			_passwordHasher = passwordHasher;
			_jwtTokenGenerator = jwtTokenGenerator;
		}

		/// <summary>
		/// Iniciar sesión
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		[ProducesResponseType(typeof(AuthResponse), 200)]
		public async Task<IActionResult> CreateToken([FromBody] LogInReq model)
		{
			try
			{
				var user = await _userRepository.WithEmail(model.Email);
				if(user == null) {
					return NotFound("Usuario con email no existe");
				}
				if (_passwordHasher.VerifyHashedPassword(user, user.Password, model.Password) != PasswordVerificationResult.Success){
					return Unauthorized();
				}

				var claims = new[] {
					new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
					new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
					new Claim(JwtRegisteredClaimNames.Email, user.Email)
				};
				var jwtSecurityToken = _jwtTokenGenerator.CreateToken(claims);

				return Ok(new AuthResponse { 
					Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
					Expiration = jwtSecurityToken.ValidTo,
					User = new UserResponse(user)
				});
			}
			catch (Exception ex)
			{
				_logger.LogError($"error while creating token: {ex}");
				return StatusCode((int)HttpStatusCode.InternalServerError, "error while creating token");
			}
		}

	    [HttpPost("reset")]
	    public async Task<IActionResult> ResetPassword([FromBody] ResetRequest request)
	    {
		    try
		    {
			    var user = await _userRepository.WithEmail(request.email);
			    var password = PasswordGenerator.CreateRandomPassword(8);
			    user.Password = _passwordHasher.HashPassword(user, password);
			    var result = await _userRepository.Update(user);
			    if (result > 0)
			    {
				    bool response = MailsHelpers.MailPassword(user.Email,password);
				    if (response)
				    {
					    return Ok();
				    }
				    else
				    {
					    return new JsonResult(new ResponseMail("Error al mandar correo",0,400)){StatusCode = 400};
				    }
				    
			    }
			    else
			    {
				    return new JsonResult(new ResponseMail("Error al actualizar",0,404)){StatusCode = 404};
			    }
			    
		    }
		    catch (Exception e)
		    {
			    return new JsonResult(e.Message){StatusCode = 500};
		    }

	    }
	}
}