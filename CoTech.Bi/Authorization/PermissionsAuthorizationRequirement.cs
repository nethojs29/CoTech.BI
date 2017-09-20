using System.Collections.Generic;
using CoTech.Bi.Core.Permissions.Models;
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