using System;
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
    public class RequiresPermissionAttribute : TypeFilterAttribute
    {
        public RequiresPermissionAttribute(params string[] permissions)
          : base(typeof(RequiresPermissionAttributeImpl))
        {
            Arguments = new[] { new PermissionsAuthorizationRequirement(permissions) };
        }

        private class RequiresPermissionAttributeImpl : Attribute, IAsyncActionFilter
        {
            private readonly ILogger _logger;
      private readonly UserManager<UserEntity> userManager;
      private readonly PermissionRepository permissionRepo;
            private readonly PermissionsAuthorizationRequirement _requiredPermissions;

            public RequiresPermissionAttributeImpl(ILogger<RequiresPermissionAttribute> logger,
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
                if(context.ActionArguments.ContainsKey("company")){
                    var companyId = context.ActionArguments["company"] as long?;
                    if (companyId.HasValue){
                        var result = await permissionRepo.GetUserPermissionInCompany(
                            Int64.Parse(userManager.GetUserId(context.HttpContext.User)), 
                            companyId.Value);
                        if (result == null) {
                            context.Result = new ChallengeResult();
                            return;
                        }
                    }
                }
                await next();

            }
        }
    }
}