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

            public RequiresRootAttributeImpl(ILogger<RequiresRoleAttribute> logger,
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
                var userId = userManager.GetUserId(context.HttpContext.User);
                if(userId == null){
                  context.Result = new ChallengeResult();
                  return;
                }
                var isRoot = await permissionRepo.IsUserRoot(Int64.Parse(userId));
                if(!isRoot){
                    context.Result = new ChallengeResult();
                    return;
                }
                await next();
            }
        }
    }
}