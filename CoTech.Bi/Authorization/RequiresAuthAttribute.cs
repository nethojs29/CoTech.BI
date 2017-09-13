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
    /// <summary>
    /// Requiere que el usuario esté logeado (Authorization Header)
    /// </summary>
    public class RequiresAuthAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// Requiere que el usuario esté logeado (Authorization Header)
        /// </summary>
        public RequiresAuthAttribute()
            : base(typeof(RequiresAuthAttributeImpl))
        {
        }

        private class RequiresAuthAttributeImpl : Attribute, IAsyncActionFilter
        {

            public async Task OnActionExecutionAsync(ActionExecutingContext context,
                ActionExecutionDelegate next)
            {
                var userId = context.HttpContext.UserId();
                if(userId == null){
                    context.Result = new UnauthorizedResult();
                    return;
                }
                await next();
            }
        }
    }
}