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
        private readonly IAuthorizationService _authorizationService;

        public OperationsController(IAuthorizationService authorizationService)
        {
            this._authorizationService = authorizationService;
        }

        public async Task<IActionResult> Open()
        {

            var wookieBox = new WookieBox();   // get wookie box from db
            var requrment = new OperationAuthorizationRequirement()
            {
                Name = WookieBoxOperation.Open
            };

            //await _authorizationService.AuthorizeAsync(User, null, requrment);
            await _authorizationService.AuthorizeAsync(User, wookieBox, requrment);
            return View();
        }
    }

    public class WokkieBoxAuthorizationHandler :
        AuthorizationHandler<OperationAuthorizationRequirement, WookieBox>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            OperationAuthorizationRequirement requirement,
            WookieBox wookieBox)
        {
            if(requirement.Name == WookieBoxOperation.Look)
            {
                if (context.User.Identity.IsAuthenticated)
                {
                    context.Succeed(requirement);
                }
            }
            else if(requirement.Name == WookieBoxOperation.Look)
            {
                if(context.User.HasClaim("Friend", "Good"))
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }


    public static class WookieBoxOperation
    {
        public static string Open = "Open";
        public static string TakeBox = "TakeBox";
        public static string ComeHere = "ComeHere";
        public static string Look = "Look";


    }


    public class WookieBox
    {
        public string Name { get; set; }
    }
}
