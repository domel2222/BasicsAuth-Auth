﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        //private readonly HttpClient _client;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            //_client = httpClientFactory.CreateClient(); 
        }
        public IActionResult Index()
        {
            return View();
        }


        [Authorize]
        public async Task<IActionResult> Secret()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            //var refreshToken = await HttpContext.GetTokenAsync("refresh_token");

            var serverClient = _httpClientFactory.CreateClient();

            serverClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            var serverResponse = await serverClient.GetAsync("https://localhost:44363/secret/index");

            await RefreshAccessToken();

            var apiClient = _httpClientFactory.CreateClient();

            token = await HttpContext.GetTokenAsync("access_token");


            apiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            var apiResponse = await apiClient.GetAsync("https://localhost:44373/secret/index");

            return View();
        }
        // add Flurl
        // we have configured our mechanisim  for requestin a new access token and we we are going to be passing refresh token 
        public async Task<string> RefreshAccessToken()
        {
        

            var refreshToken = await HttpContext.GetTokenAsync("refresh_token");

            var refreshTokenCLient = _httpClientFactory.CreateClient();

            var requestData = new Dictionary<string, string>
            {
                ["grant_type"] = "refresh_token",
                ["refresh_token"] = refreshToken,
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44363/oauth/token")
            {
                Content = new FormUrlEncodedContent(requestData)
            };

            var basicCredencial = "username:password";
            var encodedCredencial = Encoding.UTF8.GetBytes(basicCredencial);
            var base64Credentials = Convert.ToBase64String(encodedCredencial);


            request.Headers.Add("Authorization", $"Basic {base64Credentials}");

            var response = await refreshTokenCLient.SendAsync(request);

            var responseString = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseString);

            var newAccessToken = responseData.GetValueOrDefault("access_token");
            var newRefreshToken = responseData.GetValueOrDefault("refresh_token");


            var authInfo = await HttpContext.AuthenticateAsync("ClientCookie");

            authInfo.Properties.UpdateTokenValue("access_token", newAccessToken);
            authInfo.Properties.UpdateTokenValue("refresh_token", newRefreshToken);



            await HttpContext.SignInAsync("ClientCookie", authInfo.Principal, authInfo.Properties);

            return "";
            
        }


    }
}
