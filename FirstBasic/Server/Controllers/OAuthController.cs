using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Server;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class OAuthController : Controller
    {
        [HttpGet]
        public IActionResult Authorize(
            string response_type, //authorization flow type
            string client_id,  // client id 
            string redirect_uri,
            string scope,  // what info I want = email , cookie , tel
            string state) // random string generated to confirm that we are going to back to the same client
        {
            var query = new QueryBuilder();
            query.Add("redirectUri", redirect_uri);
            query.Add("state", state);
            
            
            return View(model: query.ToString());
        }

        [HttpPost]
        public IActionResult Authorize(
            string username,
            string redirectUri,
            string state)
        {
            const string code = "LaLALaLA";

            var query = new QueryBuilder();
            query.Add("code", code);
            query.Add("state", state);
            return Redirect($"{redirectUri}{query.ToString()}");
            //return View();
        }
        public async Task<IActionResult> Token(
            string grant_type, //flow of access_token request
            string code,  // confirmation of the authentication 
            string redirect_uri,
            string client_id)
        {

            // some mechanism for validation the code
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "some_id"),
                new Claim("wookie", "cookie")

            };

            //JsonConvert.DeserializeObject<TypeFilterAttribute>


            var secretBytes = Encoding.UTF8.GetBytes(Constants.Secret);
            var key = new SymmetricSecurityKey(secretBytes);
            var algorithm = SecurityAlgorithms.HmacSha256;


            var signingCredencial = new SigningCredentials(key, algorithm);

            var token = new JwtSecurityToken(
                Constants.Issuer,
                Constants.Audiance,
                claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(1),
                signingCredencial
                );

            var access_token = new JwtSecurityTokenHandler().WriteToken(token);


            var responseObject = new
            {
                access_token,
                token_type = "Bearer",
                raw_claim = "oauthTest"
            };


            var responseJson = JsonConvert.SerializeObject(responseObject);

            var responseBytes = Encoding.UTF8.GetBytes(responseJson);

            await Response.Body.WriteAsync(responseBytes, 0, responseBytes.Length);

            return Redirect(redirect_uri);
            //return Ok(new { access_token = tokenJson });
            //return View();
        }

    }
}
