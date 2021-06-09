using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basics.CustomPolicyProvider
{
    public class SecurityLevel : AuthorizeAttribute
    {
        public SecurityLevel(int level)
        {
            Policy = $"{DynamicPolicies.SecurityLevel}.{level}";
        }
    }
    //{type}
    public static class DynamicPolicies
    {
        public static IEnumerable<string> Get()
        {

            yield return SecurityLevel;
            yield return Rank;

        }
        public const string SecurityLevel = "SecurityLevel";
        public const string Rank = "Rank";
    }

    public static class DynamicAuthorizationPolicyFactory
    {
        public static AuthorizationPolicy Create(string policyName)
        {
            var parts = policyName.Split('.');
            var type = parts.First();
            var value = parts.Last();
            //switch (type)
            //{
            //    case DynamicPolicies.Rank:
            //        return new AuthorizationPolicyBuilder()
            //                .RequireClaim("Rank", value)
            //                .Build();
            //}

            var typeDynamic = (type switch
            {
                DynamicPolicies.Rank => new AuthorizationPolicyBuilder()
                                            .RequireClaim("Rank", value)
                                            .Build(),

                DynamicPolicies.SecurityLevel => new AuthorizationPolicyBuilder()
                                                .AddRequirements(new SecurityLevelRequirement(Convert.ToInt32(value)))
                                                .Build(),
                //_ => throw new NotImplementedException(),
                _ => null,

            });
            return typeDynamic;
        }
    }

    public class SecurityLevelRequirement : IAuthorizationRequirement
    {
        public int Level { get; }
        public string TestMy { get; }
        public SecurityLevelRequirement(int level)
        {
            Level = level;
        }
    }
    public class SecuriityLevelHandler : AuthorizationHandler<SecurityLevelRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            SecurityLevelRequirement requirement)
        {
            var claimValue = Convert.ToInt32(context.User.Claims
                                .FirstOrDefault(x => x.Type == DynamicPolicies.SecurityLevel)
                                    ?.Value ?? "0");

            if (requirement.Level >= claimValue)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }

    public class CustomAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        public CustomAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
        {
        }
        //{type}.{value}
        public override Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {

            foreach (var customPolicy in DynamicPolicies.Get())
            {
                if (policyName.StartsWith(customPolicy))
                {
                    //var policy = new AuthorizationPolicyBuilder().Build();
                    var policy = DynamicAuthorizationPolicyFactory.Create(policyName);

                    return Task.FromResult(policy);
                }
            }

            return base.GetPolicyAsync(policyName);
        }
    }
}
