using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;

namespace AssetsPro.Filters
{
    public class PermissionPolicyProvider : IAuthorizationPolicyProvider
    {
        public DefaultAuthorizationPolicyProvider fallBackPolicyProvider { get; }
        public PermissionPolicyProvider(IOptions<AuthorizationOptions> option)
        {
            fallBackPolicyProvider = new DefaultAuthorizationPolicyProvider(option);
        }
        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
            return fallBackPolicyProvider.GetDefaultPolicyAsync();
        }

        public Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
        {
            return fallBackPolicyProvider.GetDefaultPolicyAsync();
        }

        public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            if (policyName.StartsWith("Permission",StringComparison.OrdinalIgnoreCase)) 
            {
                var policy = new AuthorizationPolicyBuilder();
                policy.AddRequirements(new PermissionRequirment(policyName));
                return Task.FromResult(policy.Build());
            }
            return fallBackPolicyProvider.GetPolicyAsync(policyName);
        }
    }
}
