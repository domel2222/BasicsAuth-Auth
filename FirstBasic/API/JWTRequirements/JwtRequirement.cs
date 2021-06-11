using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace API.JWTRequirements
{
    public class JwtRequirement : IAuthorizationRequirement
    {
        
    }

    public class JwtRequirementHandler : AuthorizationHandler<JwtRequirement>
    {
        private readonly HttpClient _client;
        private readonly  HttpContext _httpContext;

        public JwtRequirementHandler(IHttpClientFactory httpClientFactory,
                                       IHttpContextAccessor httpContextAccessor)
        {
            _client = httpClientFactory.CreateClient();
            _httpContext = httpContextAccessor.HttpContext;
        }
        protected override Task HandleRequirementAsync(
                AuthorizationHandlerContext context, 
                JwtRequirement requirement)
        {
            
        }
    }
}
