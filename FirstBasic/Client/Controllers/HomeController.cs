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
        private readonly HttpClient _client;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _client = httpClientFactory.CreateClient(); 
        }
        public IActionResult Index()
        {
            return View();
        }


        [Authorize]
        public async Task<IActionResult> Secret()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var refreshToken = await HttpContext.GetTokenAsync("refresh_token");

            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            var serverResponse = await _client.GetAsync("https://localhost:44363/secret/index");

            var apiResponse = await _client.GetAsync("https://localhost:44373/secret/index");

            return View();
        }
        // add Flurl
        public async Task<string> RefreshAccessToken()
        {
        https://localhost:44373/secret/index

            var refreshToken = await HttpContext.GetTokenAsync("refresh_token");

            var refreshTokenCLient = _httpClientFactory.CreateClient();

            var requestData = new Dictionary<string, string>
            {
                ["grant_type"] = "refresh_token",
                ["refresh_token"] = refreshToken,
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44373/oauth/token")
            {
                Content = new FormUrlEncodedContent(requestData)
            };

            var basicCredencial = "username:password";
            var encodedCredencial = Encoding.UTF8.GetBytes(basicCredencial);
            var base64Credentials = Convert.ToBase64String(encodedCredencial);


            request.Headers.Add("Authorization", $"Basic {base64Credentials}");

            var response = await refreshTokenCLient.SendAsync(request);

            return "";
            
        }


    }
}
