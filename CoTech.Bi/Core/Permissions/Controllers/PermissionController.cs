using System.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Authorization;
using CoTech.Bi.Core.Permissions.Model;
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

        // [HttpGet("permissions")]
        // [RequiresAbsoluteRole(Role.Super, Role.Admin, Role.Reader)]
        // public async Task<IActionResult> GetUsersPermissions(long company) {
        //     var permissions = await permissionRepository.Get
        // }

        [HttpPost("{user}")]
        [RequiresAbsoluteRole(Role.Super, Role.Admin)]
        [ProducesResponseType(typeof(PermissionResponse), 200)]
        public async Task<IActionResult> GivePermission(long company, long user, [FromBody] CreatePermissionReq req) {
            if(!ModelState.IsValid) return BadRequest();
            var permission = new PermissionEntity { CompanyId = company, UserId = user, RoleId = req.RoleId.Value };
            await permissionRepository.Create(permission);
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
            var permission = await permissionRepository.FindOne(company, user, role);
            if(permission == null){
                return NotFound();
            }
            await permissionRepository.Delete(permission);
            return Ok();
        }

        [HttpDelete("{user}")]
        [RequiresAbsoluteRole(Role.Super, Role.Admin)]
        public async Task<IActionResult> RevokePermissions(long company, long user) {
            var permissions = await permissionRepository.GetUserPermissionsInCompany(user, company);
            if(permissions.Count == 0){
                return NotFound();
            }
            await permissionRepository.Revoke(permissions);
            return Ok();
        }
    }
}