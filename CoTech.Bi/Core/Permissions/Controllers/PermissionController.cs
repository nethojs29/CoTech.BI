using System;
using System.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Authorization;
using CoTech.Bi.Core.Permissions.Models;
using CoTech.Bi.Core.Permissions.Repositories;
using CoTech.Bi.Core.Users.Models;
using CoTech.Bi.Core.Users.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CoTech.Bi.Core.Permissions.Controllers
{
    [Route("/api/companies/{company}/users")]
    public class PermissionController : Controller
    {
        private readonly PermissionRepository permissionRepository;
        private readonly UserRepository userRepository;

        public PermissionController(PermissionRepository permissionRepository, UserRepository userRepository){
            this.permissionRepository = permissionRepository;
            this.userRepository = userRepository;
        }

        [HttpGet]
        [RequiresAnyRole]
        public async Task<IActionResult> GetUsersInCompany(long company) {
            var users = await userRepository.InCompany(company);
            return Ok(users.Select(u => new UserResponse(u)));
        }

        [HttpGet("withPermissions")]
        [RequiresAbsoluteRole(Role.Super, Role.Admin, Role.Reader)]
        public async Task<IActionResult> GetWithUsersPermissions(long company) {
            var users = await userRepository.InCompany(company);
            return Ok(users.Select(u => new UserAndPermissions(u)));
        }

        [HttpGet("{userId}/permissions")]
        public async Task<IActionResult> GetUserPermissions(long userId){
            return new OkObjectResult(await permissionRepository.GetUserPermissions(userId));
        }

        [HttpPost("{user}")]
        [RequiresAbsoluteRole(Role.Super, Role.Admin)]
        [ProducesResponseType(typeof(PermissionResponse), 200)]
        public async Task<IActionResult> GivePermission(long company, long user, [FromBody] CreatePermissionReq req) {
            var cmd = new GivePermissionCmd(company, HttpContext.UserId().Value, req, user);
            var permission = await permissionRepository.Create(cmd);
            return Ok(new PermissionResponse(permission));
        }

        /// <summary>
        /// Elimina un rol a un usuario
        /// </summary>
        /// <param name="company">id de empresa</param>
        /// <param name="user">id de usuario</param>
        /// <param name="role">id de rol</param>
        /// <returns></returns>
        /// <response code="200">Role eliminado</response>
        [HttpDelete("{user}/{role}")]
        [RequiresAbsoluteRole(Role.Super, Role.Admin)]
        public async Task<IActionResult> RemoveRole(long company, long user, long role) {
            var cmd = new RemoveRoleCmd(company, HttpContext.UserId().Value, role, user);
            var ok = await permissionRepository.Delete(cmd);
            if(ok)
                return Ok();
            return BadRequest();
        }

        [HttpDelete("{user}")]
        [RequiresAbsoluteRole(Role.Super, Role.Admin)]
        public async Task<IActionResult> RevokePermissions(long company, long user) {
            var cmd = new RevokePermissionsCmd(company, HttpContext.UserId().Value, user);
            var ok = await permissionRepository.Revoke(cmd);
            if(ok)
                return Ok();
            return BadRequest();
        }
    }
}