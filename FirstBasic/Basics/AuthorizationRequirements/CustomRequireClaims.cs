using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basics.AuthorizationRequirements
{
    public class CustomRequireClaims : IAuthorizationRequirement
    {
        public CustomRequireClaims(string claimType)
        {
            this.ClaimJhonesType = claimType;
        }

        public string ClaimJhonesType { get; }
    }
}
