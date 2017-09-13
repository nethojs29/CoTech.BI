using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using CoTech.Bi.Core.Permissions.Model;
using Microsoft.AspNetCore.Identity;
using CoTech.Bi.Core.Users.Models;
using CoTech.Bi.Core.Permissions.Repositories;

namespace CoTech.Bi.Authorization
{
    /// <summary>
    /// Requiere al usuario tener alguno de los roles dados en la empresa especificada en la ruta
    /// </summary>
    public class RequiresAbsoluteRoleAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// Requiere al usuario tener alguno de los roles dados en la empresa especificada en la ruta,
        /// a diferencia de RequiresRoleAttribute, este no permite entrar a usuarios con roles SUPER,
        /// ADMIN ni READER al menos que sean especificados.
        /// Sin embargo, un usuario root si puede entrar
        /// </summary>
        /// <param name="permissions">Los roles que requiere el usuario</param>
        public RequiresAbsoluteRoleAttribute(params long[] permissions)
          : base(typeof(RequiresAbsoluteRoleAttributeImpl))
        {
            Arguments = new[] { new PermissionsAuthorizationRequirement(permissions) };
        }

        private class RequiresAbsoluteRoleAttributeImpl : Attribute, IAsyncActionFilter
        {
            private readonly ILogger _logger;
            private readonly PermissionRepository permissionRepo;
            private readonly PermissionsAuthorizationRequirement _requiredPermissions;

            public RequiresAbsoluteRoleAttributeImpl(ILogger<RequiresAbsoluteRoleAttribute> logger,
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
                if(context.ActionArguments.Count == 0) {
                    context.Result = new UnauthorizedResult();
                    return;
                }
                var companyId = context.ActionArguments.First().Value as Guid?;
                if(!companyId.HasValue){
                    context.Result = new UnauthorizedResult();
                    return;
                }
                var hasRole = await permissionRepo.UserHasAtLeastOneRoleInCompany(
                    userId.Value,
                    companyId.Value, 
                    _requiredPermissions.RequiredRoles,
                    true, true
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