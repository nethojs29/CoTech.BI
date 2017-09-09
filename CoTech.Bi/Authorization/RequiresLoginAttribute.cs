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
    public class RequiresLoginAttribute : TypeFilterAttribute
    {
        public RequiresLoginAttribute()
            : base(typeof(RequiresLoginAttributeImpl))
        {
        }

        private class RequiresLoginAttributeImpl : Attribute, IAsyncActionFilter
        {
            private readonly UserManager<UserEntity> userManager;

            public RequiresLoginAttributeImpl(UserManager<UserEntity> userManager)
            {
                this.userManager = userManager;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context,
                ActionExecutionDelegate next)
            {
                var userId = context.HttpContext.UserId();
                if(userId == -1){
                    context.Result = new UnauthorizedResult();
                    return;
                }
                await next();
            }
        }
    }
}