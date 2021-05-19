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
        private readonly SignInManager<IdentityUser> _signInManager;

        public HomeController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
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
        public async Task<IActionResult> Login(string userName, string password)
        {
            //login functionality

            var user = await _userManager.FindByNameAsync(userName);
            if(user != null)
            {
                //sign in 
                var signInResult = await _signInManager.PasswordSignInAsync(user, password, false, false);
                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Register(string userName, string password)
        {
            //register functionality
            var user = new IdentityUser
            {
                UserName = userName,
                Email = "",
            };

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                //sign in 
                var signInResult = await _signInManager.PasswordSignInAsync(user, password, false, false);
                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
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

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index");
        }
    }
}
