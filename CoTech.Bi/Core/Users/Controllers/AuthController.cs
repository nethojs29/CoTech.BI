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
using CoTech.Bi.Core.Permissions.Models;
using CoTech.Bi.Util;
using CoTech.Bi.Core.Users.Repositories;
using Newtonsoft.Json;
using CoTech.Bi.Core.Permissions.Repositories;
using CoTech.Bi.Core.Companies.Repositories;
using CoTech.Bi.Core.Companies.Models;
using System.Collections.Generic;
using CoTech.Bi.Authorization;

namespace CoTech.Bi.Core.Users.Controllers
{
	[Route("api/auth")]
	public class AuthController : Controller
    {
		private readonly UserRepository _userRepository;
    private readonly PermissionRepository permissionRepository;
    private readonly CompanyRepository companyRepository;
    private IPasswordHasher<UserEntity> _passwordHasher;
		private JwtTokenGenerator _jwtTokenGenerator;
		private ILogger<AuthController> _logger;

		public AuthController(UserRepository userRepository,
													PermissionRepository permissionRepository,
													CompanyRepository companyRepository,
													IPasswordHasher<UserEntity> passwordHasher, 
													JwtTokenGenerator jwtTokenGenerator, 
													ILogger<AuthController> logger)
		{
			_userRepository = userRepository;
      this.permissionRepository = permissionRepository;
      this.companyRepository = companyRepository;
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
					User = new UserResponse(user),
					IAmRoot = await permissionRepository.UserIsRoot(user.Id),
					Permissions = (await permissionRepository.GetUserPermissions(user.Id)).Select(p => new PermissionResponse(p)).ToList(),
					Companies = (await companyRepository.GetUserCompanies(user.Id)).Select(c => new CompanyResult(c)).ToList()
				});
			}
			catch (Exception ex)
			{
				_logger.LogError($"error while creating token: {ex}");
				return StatusCode((int)HttpStatusCode.InternalServerError, "error while creating token");
			}
		}

		[HttpGet]
		[RequiresAuth]
		public async Task<IActionResult> GetMyInfo() {
			var userId = HttpContext.UserId().Value;
			var user = await _userRepository.WithId(userId);
			var claims = new[] {
					new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
					new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
					new Claim(JwtRegisteredClaimNames.Email, user.Email)
			};
			var jwtSecurityToken = _jwtTokenGenerator.CreateToken(claims);
			return Ok(new AuthResponse {
					Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
					Expiration = jwtSecurityToken.ValidTo,
					User = new UserResponse(user),
					IAmRoot = await permissionRepository.UserIsRoot(userId),
					Permissions = (await permissionRepository.GetUserPermissions(userId)).Select(p => new PermissionResponse(p)).ToList(),
					Companies = (await companyRepository.GetUserCompanies(userId)).Select(c => new CompanyResult(c)).ToList()
				});
		}

			/// <summary>
			/// PRE-Alpha v-0.10
			/// </summary>
			/// <param name="request"></param>
			/// <returns></returns>
	    [HttpPost("reset")]
	    public async Task<IActionResult> ResetPassword([FromBody] ResetRequest request)
	    {
		    try
		    {
			    var user = await _userRepository.WithEmail(request.email);
			    var password = PasswordGenerator.CreateRandomPassword(8);
			    user.Password = _passwordHasher.HashPassword(user, password);
					var cmd = new ChangePasswordCmd(user.Password, user.Id);
			    var result = await _userRepository.ChangePassword(cmd);
			    if (result)
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