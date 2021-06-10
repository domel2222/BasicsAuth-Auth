using Microsoft.AspNetCore.Http.Extensions;
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
            return Redirect("");
            //return View();
        }
        public IActionResult Token()
        {
            
            return View();
        }

    }
}
