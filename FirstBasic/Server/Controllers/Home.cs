using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Server.Controllers
{
    public class Home : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        
        public IActionResult Authenticate()
        {

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "some_id"),
                new Claim("wookie", "cookie")

            };

            //JsonConvert.DeserializeObject<TypeFilterAttribute>

            var token = new JwtSecurityToken();
            return RedirectToAction("Index");
        }


    }
}
