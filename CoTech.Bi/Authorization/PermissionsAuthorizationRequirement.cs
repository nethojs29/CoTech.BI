using System.Collections.Generic;
using CoTech.Bi.Core.Permissions.Model;
using Microsoft.AspNetCore.Authorization;

namespace CoTech.Bi.Authorization
{
    public class PermissionsAuthorizationRequirement : IAuthorizationRequirement
    {
        public IEnumerable<long> RequiredRoles { get; }

        public PermissionsAuthorizationRequirement(IEnumerable<long> requiredRoles)
        {
            RequiredRoles = requiredRoles;
        }
    }
}