using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basics.Controllers
{
    public class OperationsController : Controller
    {
        
    }

    public class WokkieBoxAuthorizationHandler :
        AuthorizationHandler<OperationAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            OperationAuthorizationRequirement requirement)
        {
            
        }
    }


    public static class WookieBoxOperation
    {
        public static string Open = "Open";
        public static string TakeBox = "TakeBox";
        public static string ComeHere = "ComeHere";
        public static string Look = "Look";


    }
}
