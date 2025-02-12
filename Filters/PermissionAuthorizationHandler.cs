using Microsoft.AspNetCore.Authorization;

namespace AssetsPro.Filters
{
    public class PermissionAutorizationHandler : AuthorizationHandler<PermissionRequirment>
    {
        public PermissionAutorizationHandler() { }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirment requirement)
        {
            if (context.User == null)
                return;
            var canAcces = context.User.Claims.Any(c => c.Type == "Permission" && c.Value == requirement.Permission && c.Issuer == "LOCAL AUTHORITY");
            if (canAcces)
            {
                context.Succeed(requirement);
                return;
            }
        }
    }
}
