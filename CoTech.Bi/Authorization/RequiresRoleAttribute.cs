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

namespace CoTech.Bi.Authorization
{
    public class RequiresRoleAttribute : TypeFilterAttribute
    {
        public RequiresRoleAttribute(params long[] permissions)
          : base(typeof(RequiresRoleAttributeImpl))
        {
            Arguments = new[] { new PermissionsAuthorizationRequirement(permissions) };
        }

        private class RequiresRoleAttributeImpl : Attribute, IAsyncActionFilter
        {
            private readonly ILogger _logger;
            private readonly UserManager<UserEntity> userManager;
            private readonly PermissionRepository permissionRepo;
            private readonly PermissionsAuthorizationRequirement _requiredPermissions;

            public RequiresRoleAttributeImpl(ILogger<RequiresRoleAttribute> logger,
                                            UserManager<UserEntity> userManager,
                                            PermissionRepository permissionRepo,
                                            PermissionsAuthorizationRequirement requiredPermissions)
            {
                _logger = logger;
                this.userManager = userManager;
                this.permissionRepo = permissionRepo;
                _requiredPermissions = requiredPermissions;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context,
                                                     ActionExecutionDelegate next)
            {
                var userId = context.HttpContext.UserId();
                if(userId == -1){
                  context.Result = new UnauthorizedResult();
                  return;
                }
                if(context.ActionArguments.Count == 0) {
                    context.Result = new UnauthorizedResult();
                    return;
                }
                var companyId = context.ActionArguments.First().Value as long?;
                if(!companyId.HasValue){
                    context.Result = new UnauthorizedResult();
                    return;
                }
                _requiredPermissions.RequiredRoles.Append(Role.Super);
                _requiredPermissions.RequiredRoles.Append(Role.Admin);
                if(context.HttpContext.Request.Method == "GET")
                  _requiredPermissions.RequiredRoles.Append(Role.Reader);
                var hasRole = await permissionRepo.UserHasAtLeastOneRoleInCompany(
                    userId,
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