using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basics.AuthorizationRequirements
{
    public class CustomRequireClaimsHandler : AuthorizationHandler<CustomRequireClaims>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            CustomRequireClaims requirement)
        {
            var hasClaim = context.User.Claims.Any(x => x.Type == requirement.ClaimJhonesType);
            if (hasClaim)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

    public static class AuthorizationPolicyBuilderExtensions
    {
        public static AuthorizationPolicyBuilder RequireCustomClaim(
            this AuthorizationPolicyBuilder builder, string claimType
            )
        {
            builder.AddRequirements(new CustomRequireClaims(claimType));

            return builder;
        }
    }
}
