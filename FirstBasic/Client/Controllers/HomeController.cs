using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient __client;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            __client = httpClientFactory.CreateClient(); 
        }
        public IActionResult Index()
        {
            return View();
        }


        [Authorize]
        public async Task<IActionResult> Secret()
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            __client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            var serverResponse = await __client.GetAsync("https://localhost:44363/");

            return View();
        }
    }
}
