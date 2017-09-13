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
using CoTech.Bi.Core.Permissions.Repositories;

namespace CoTech.Bi.Authorization
{
    /// <summary>
    /// Requiere al usuario que tenga cualquier rol en la empresa que se especifica en la ruta, o que sea root
    /// </summary>
    public class RequiresAnyRoleAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// Requiere al usuario que tenga cualquier rol en la empresa que se especifica en la ruta.
        /// Un usuario root puede entrar a esta ruta
        /// Un usuario con rol super en alguna empresa padre de la empresa especificada puede entrar
        /// </summary>
        public RequiresAnyRoleAttribute()
          : base(typeof(RequiresAnyRoleAttributeImpl)) {}

        private class RequiresAnyRoleAttributeImpl : Attribute, IAsyncActionFilter
        {
            private readonly ILogger _logger;
            private readonly PermissionRepository permissionRepo;

            public RequiresAnyRoleAttributeImpl(ILogger<RequiresAnyRoleAttribute> logger,
                                            PermissionRepository permissionRepo)
            {
                _logger = logger;
                this.permissionRepo = permissionRepo;
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
                var hasAnyRole = await permissionRepo.UserHasAnyRoleInCompany(userId.Value, companyId.Value, true, true);
                if(!hasAnyRole){
                    context.Result = new UnauthorizedResult();
                    return;
                }
                await next();
            }
        }
    }
}