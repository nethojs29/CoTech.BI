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
    /// Requiere al usuario tener alguno de los roles dados en la empresa especificada en la ruta.
    /// </summary>
    public class RequiresRoleAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// Requiere al usuario tener alguno de los roles dados en la empresa especificada en la ruta.
        /// Un usuario root puede entrar.
        /// Un usuario con rol super en la empresa especificada o en alguna empresa padre de esta empresa puede entrar
        /// Un usuario con rol admin en la empresa especificada puede entrar
        /// Un usuario con rol reader en la empresa especificada puede entrar si el método de la ruta es GET
        /// </summary>
        /// <param name="permissions">Los roles que requiere el usuario</param>
        public RequiresRoleAttribute(params long[] permissions)
          : base(typeof(RequiresRoleAttributeImpl))
        {
            Arguments = new[] { new PermissionsAuthorizationRequirement(permissions) };
        }

        private class RequiresRoleAttributeImpl : Attribute, IAsyncActionFilter
        {
            private readonly PermissionRepository permissionRepo;
            private readonly PermissionsAuthorizationRequirement _requiredPermissions;

            public RequiresRoleAttributeImpl(PermissionRepository permissionRepo,
                                            PermissionsAuthorizationRequirement requiredPermissions)
            {
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
                _requiredPermissions.RequiredRoles.Append(Role.Super);
                _requiredPermissions.RequiredRoles.Append(Role.Admin);
                if(context.HttpContext.Request.Method == "GET")
                  _requiredPermissions.RequiredRoles.Append(Role.Reader);
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