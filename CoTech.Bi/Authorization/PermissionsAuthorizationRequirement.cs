using System.Collections.Generic;
using CoTech.Bi.Core.Permissions.Model;
using Microsoft.AspNetCore.Authorization;

namespace CoTech.Bi.Authorization
{
    public class PermissionsAuthorizationRequirement : IAuthorizationRequirement
    {
        public IEnumerable<string> RequiredPermissions { get; }

        public PermissionsAuthorizationRequirement(IEnumerable<string> requiredPermissions)
        {
            RequiredPermissions = requiredPermissions;
        }
    }
}