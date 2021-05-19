using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(UserManager<IdentityUser> userManager)
        {
            this._userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }
        public IActionResult Login(string userName, string password)
        {
           //login functionality
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Register(string userName, string password)
        {
            //register functionality
            var user = new IdentityUser
            {
                UserName = userName,
                Email = "",

            }

            _userManager.CreateAsync();
            return RedirectToAction("Index");
        }

        public IActionResult Login()
        {

                return View();
        }
        public IActionResult Register()
        {
            return  View();
        }
    }
}
