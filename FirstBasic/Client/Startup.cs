using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(config =>
            {
                // we check the cookie to confirm that we are authenticated
                config.DefaultAuthenticateScheme = "ClientCookie";
                // when we sign in we will deal out a cookie
                config.DefaultSignInScheme = "ClientCookie";
                // use this to check if we are allowed to do something
                config.DefaultChallengeScheme = "OurServer";
            })
                .AddCookie("ClientCookie")
                .AddOAuth("OurServer", config =>
                {
                    config.ClientId = "client_id";
                    config.ClientSecret = "client_secret";
                    config.CallbackPath = "/oauth/callback";
                    config.AuthorizationEndpoint = "https://localhost:44363/oauth/authorize";
                    config.TokenEndpoint = "https://localhost:44363/oauth/token";

                    config.SaveTokens = true;
                
                });

            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseRouting();


            app.UseAuthentication();
  
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();

            });
        }
    }
}
