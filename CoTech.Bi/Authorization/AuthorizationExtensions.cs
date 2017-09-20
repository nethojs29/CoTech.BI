using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CoTech.Bi.Authorization
{
    public static class AuthorizationExtensions {
        public static long? UserId(this HttpContext context){
            var userClaim = context.User.FindFirstValue("sub");
            if(userClaim != null) return Int64.Parse(userClaim);
            string authHeader = context.Request.Headers["Authorization"];
            if(authHeader == null || !authHeader.StartsWith("Bearer ")){
                return null;
            }
            var token = authHeader.Substring(7);
            var tokenHandler = new JwtSecurityTokenHandler();
            if(!tokenHandler.CanReadToken(token)){
                return null;
            }
            try {
                var readToken = tokenHandler.ReadJwtToken(token);
                DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                var expiration = origin + new TimeSpan(0, 0, readToken.Payload.Exp ?? 0);
                if(expiration < DateTime.UtcNow) throw new Exception("Token expired");
                var claimsIdentity = new ClaimsIdentity(readToken.Claims);
                context.User.AddIdentity(claimsIdentity);
                return Int64.Parse(readToken.Subject);
            } catch(Exception){
                return null;
            }
        }

    }
}