using Basics.CustomPolicyProvider;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Basics.Controllers
{
    public class Home : Controller
    {
        //private readonly IAuthorizationService _authorizationService;

        //public Home(IAuthorizationService authorizationService)
        //{
        //    this._authorizationService = authorizationService;
        //}

        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        [Authorize(Policy = "Claim.DoB")]
        //[Authorize(Policy = "SecurityLevel.5")]
        public IActionResult SecretPolicy()
        {
            return View("Secret");
        }


        [Authorize(Roles = "Admin")]
        public IActionResult SecretRole()
        {
            return View("Secret");
        }

        [SecurityLevel(5)]
        public IActionResult SecretLevel()
        {
            return View("Secret");
        }

        [SecurityLevel(10)]
        public IActionResult SecretHigherLevel()
        {
            return View("Secret");
        }

        [AllowAnonymous]
        public IActionResult Authenticate()
        {
            var supaClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "Bob"),
                new Claim(ClaimTypes.Email, "Bob@fmail.com"),
                new Claim(ClaimTypes.DateOfBirth, "11/11/2001"),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(DynamicPolicies.SecurityLevel, "7"),
                new Claim("Jhons.Says", "Good Boys"),
            };
            var licenseClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "Archi K Boomm"),
                new Claim("DrivingLicencse", "A+")
            };

            var personalIdentity = new ClaimsIdentity(supaClaims, "Jhons Identity");
            var licenseIdentity = new ClaimsIdentity(licenseClaims, "Government");


            var userPrincipal = new ClaimsPrincipal(new[] { personalIdentity, licenseIdentity });


            //------------------------------------------------------------------------------------------


            HttpContext.SignInAsync(userPrincipal);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DoTheDishes([FromServices] IAuthorizationService _authorizationService)  // Iauthorization localy only for this method
        {

            // we are doing stuff here 
            var builder = new AuthorizationPolicyBuilder("Schema");
            var customPolicy = builder.RequireClaim("Hello").Build();



            var authResult = await _authorizationService.AuthorizeAsync(User, customPolicy);

            if (authResult.Succeeded)
            {
                return View("Index");
            }
            return View("Index");
        }
    }
}
