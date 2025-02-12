using Microsoft.AspNetCore.Authorization;

namespace AssetsPro.Filters
{
    public class PermissionRequirment : IAuthorizationRequirement
    {
        public string Permission { get; private set; }
        public PermissionRequirment(string permission) { Permission = permission; }
    }
}
