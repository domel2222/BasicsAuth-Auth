using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class OAuthController : Controller
    {
        [HttpGet]
        public IActionResult Authorize(
            string responseType,
            string clientId,
            string redirectUri,
            string scope,
            string state)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Authorize(
            string username,
            string responseType,
            string clientId,
            string redirectUri,
            string scope,
            string state)
        {
            return View();
        }
        public IActionResult Token()
        {
            return View();
        }

    }
}
