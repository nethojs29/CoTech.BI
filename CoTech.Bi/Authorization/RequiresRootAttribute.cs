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
using Microsoft.AspNetCore.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CoTech.Bi.Authorization
{
    public class RequiresRootAttribute : TypeFilterAttribute
    {
        public RequiresRootAttribute()
          : base(typeof(RequiresRootAttributeImpl))
        {
        }

        private class RequiresRootAttributeImpl : Attribute, IAsyncActionFilter
        {
            private readonly ILogger _logger;
            private readonly UserManager<UserEntity> userManager;
            private readonly PermissionRepository permissionRepo;

            public RequiresRootAttributeImpl(ILogger<RequiresImportantRoleAttribute> logger,
                                            UserManager<UserEntity> userManager,
                                            PermissionRepository permissionRepo)
            {
                _logger = logger;
                this.userManager = userManager;
                this.permissionRepo = permissionRepo;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context,
                                                     ActionExecutionDelegate next)
            {
                var userId = context.HttpContext.UserId();
                if(userId == -1){
                  context.Result = new UnauthorizedResult();
                  return;
                }
                var isRoot = await permissionRepo.UserIsRoot(userId);
                if(!isRoot){
                    context.Result = new UnauthorizedResult();
                    return;
                }
                await next();
            }
        }
    }
}