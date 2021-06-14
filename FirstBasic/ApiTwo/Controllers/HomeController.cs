using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiTwo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }


        public async Task<IActionResult> Index()
        {
            //retrive access token

            var serverClient = _httpClientFactory.CreateClient();


            //all information about our identity server right so once 
            var discoveryDocument = await serverClient.GetDiscoveryDocumentAsync("https://localhost:44303/");


            var tokenResponse = await serverClient.RequestClientCredentialsTokenAsync(
                new ClientCredentialsTokenRequest
                {
                    Address = discoveryDocument.TokenEndpoint,

                    ClientId = "client_id",
                    ClientSecret = "client_secret",

                    Scope = "ApiOne",
                }
                );
            //retrive secret data


            return Ok(new
            {

            });
        }
    }
}
