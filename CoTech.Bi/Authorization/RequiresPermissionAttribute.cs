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
using Microsoft.AspNetCore.Http;

namespace CoTech.Bi.Authorization
{
    public class RequiresPermissionAttribute : TypeFilterAttribute
    {
        public RequiresPermissionAttribute()
          : base(typeof(RequiresPermissionAttributeImpl))
        {
        }

        private class RequiresPermissionAttributeImpl : Attribute, IAsyncActionFilter
        {
            private readonly ILogger _logger;
            private readonly UserManager<UserEntity> userManager;
            private readonly PermissionRepository permissionRepo;

            public RequiresPermissionAttributeImpl(ILogger<RequiresPermissionAttribute> logger,
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
                if(context.ActionArguments.Count == 0) {
                    context.Result = new UnauthorizedResult();
                    return;
                }
                var companyId = context.ActionArguments.First().Value as long?;
                if(!companyId.HasValue){
                    context.Result = new UnauthorizedResult();
                    return;
                }
                var hasAnyRole = await permissionRepo.UserHasAnyRoleInCompany(userId, companyId.Value, true, true);
                if(!hasAnyRole){
                    context.Result = new UnauthorizedResult();
                    return;
                }
                await next();
            }
        }
    }
}