using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using CoTech.Bi.Core.Permissions.Models;
using Microsoft.AspNetCore.Identity;
using CoTech.Bi.Core.Users.Models;
using CoTech.Bi.Core.Permissions.Repositories;

namespace CoTech.Bi.Authorization
{
    /// <summary>
    /// Requiere al usuario tener alguno de los roles dados en cualquier empresa
    /// </summary>
    public class RequiresAbsoluteRoleAnywhereAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// Requiere al usuario tener alguno de los roles dados en cualquier empresa.
        /// Un usuario root puede entrar
        /// </summary>
        /// <param name="permissions">Los roles que requiere el usuario</param>
        public RequiresAbsoluteRoleAnywhereAttribute(params long[] permissions)
          : base(typeof(RequiresAbsoluteRoleAnywhereAttributeImpl))
        {
            Arguments = new[] { new PermissionsAuthorizationRequirement(permissions) };
        }

        private class RequiresAbsoluteRoleAnywhereAttributeImpl : Attribute, IAsyncActionFilter
        {
            private readonly ILogger _logger;
            private readonly PermissionRepository permissionRepo;
            private readonly PermissionsAuthorizationRequirement _requiredPermissions;

            public RequiresAbsoluteRoleAnywhereAttributeImpl(ILogger<RequiresAbsoluteRoleAnywhereAttribute> logger,
                                            PermissionRepository permissionRepo,
                                            PermissionsAuthorizationRequirement requiredPermissions)
            {
                _logger = logger;
                this.permissionRepo = permissionRepo;
                _requiredPermissions = requiredPermissions;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context,
                                                     ActionExecutionDelegate next)
            {
                var userId = context.HttpContext.UserId();
                if(userId == null){
                  context.Result = new UnauthorizedResult();
                  return;
                }
                var hasRole = await permissionRepo.UserHasAtLeastOneRoleAnywhere(
                    userId.Value,
                    _requiredPermissions.RequiredRoles,
                    true
                );
                if(!hasRole){
                    context.Result = new UnauthorizedResult();
                    return;
                }
                await next();
            }
        }
    }
}