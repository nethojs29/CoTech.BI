using System;
using System.Threading.Tasks;
using CoTech.Bi.Authorization;
using CoTech.Bi.Core.Permissions.Model;
using CoTech.Bi.Core.Permissions.Models;
using CoTech.Bi.Core.Permissions.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CoTech.Bi.Core.Permissions.Controllers
{
    [Route("/api/companies/{company}/users")]
    public class PermissionController : Controller
    {
        private readonly PermissionRepository permissionRepository;

        public PermissionController(PermissionRepository permissionRepository){
          this.permissionRepository = permissionRepository;
        }

        [HttpPost("{user}")]
        [RequiresAbsoluteRole(Role.Super, Role.Admin)]
        [ProducesResponseType(typeof(PermissionResponse), 200)]
        public async Task<IActionResult> GivePermission(Guid company, Guid user, [FromBody] CreatePermissionReq req) {
            var permission = new PermissionEntity { CompanyId = company, UserId = user, RoleId = req.RoleId };
            await permissionRepository.Create(permission);
            return Ok(new PermissionResponse(permission));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="company"></param>
        /// <param name="user"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        /// <response code="200">Role eliminado</response>
        [HttpDelete("{user}/{role}")]
        [RequiresAbsoluteRole(Role.Super, Role.Admin)]
        public async Task<IActionResult> RemoveRole(Guid company, Guid user, long role) {
            var permission = await permissionRepository.FindOne(company, user, role);
            if(permission == null){
                return NotFound();
            }
            await permissionRepository.Delete(permission);
            return Ok();
        }

        [HttpDelete("{user}")]
        [RequiresAbsoluteRole(Role.Super, Role.Admin)]
        public async Task<IActionResult> RevokePermissions(Guid company, Guid user) {
            var permissions = await permissionRepository.GetUserPermissionsInCompany(user, company);
            if(permissions.Count == 0){
                return NotFound();
            }
            await permissionRepository.Revoke(permissions);
            return Ok();
        }
    }
}