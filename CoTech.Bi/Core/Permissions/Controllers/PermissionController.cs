using System;
using System.Threading.Tasks;
using CoTech.Bi.Authorization;
using CoTech.Bi.Core.Permissions.Models;
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
        public async Task<IActionResult> GivePermission(long company, long user, [FromBody] CreatePermissionReq req) {
            var evt = new GivePermissionCmd(company, HttpContext.UserId().Value, req, user);
            var permission = await permissionRepository.Create(evt);
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