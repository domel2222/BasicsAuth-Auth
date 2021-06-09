using Basics.AuthorizationRequirements;
using Basics.Controllers;
using Basics.CustomPolicyProvider;
using Basics.Transformers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Basics
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication("CookieAuth")
                .AddCookie("CookieAuth", config =>
                {
                    config.Cookie.Name = "My.Cookie";
                    config.LoginPath = "/Home/Authenticate";
                });

            services.AddAuthorization(configure =>
            {

                //policy under the hood
                //var defaultAuthBuilder = new AuthorizationPolicyBuilder();
                //var defaultAuthPolicy = defaultAuthBuilder
                //    .RequireAuthenticatedUser()
                //    .RequireClaim(ClaimTypes.DateOfBirth)
                //    .Build();

                //configure.DefaultPolicy = defaultAuthPolicy;

                //configure.AddPolicy("Claim.DoB", policyBuilder =>
                //{
                //    policyBuilder.RequireClaim(ClaimTypes.DateOfBirth);
                //}
                //);

                //configure.AddPolicy("Claim.DoB", policyBuilder =>
                //{
                //    policyBuilder.AddRequirements(new CustomRequireClaims(ClaimTypes.DateOfBirth));
                //}

                configure.AddPolicy("Admin", policyBuilder => policyBuilder.RequireClaim(ClaimTypes.Role, "Admin"));

                configure.AddPolicy("Claim.DoB", policyBuilder =>
                {
                    policyBuilder.RequireCustomClaim(ClaimTypes.DateOfBirth);
                }

);
            });


            services.AddControllersWithViews(config =>
            {

                
                var defaultAuthBuilder = new AuthorizationPolicyBuilder();
                var defaultAuthPolicy = defaultAuthBuilder
                    .RequireAuthenticatedUser()
                    .Build();
                //global filter
                config.Filters.Add(new AuthorizeFilter(defaultAuthPolicy));
            }
                );

            services.AddScoped<IAuthorizationHandler, CustomRequireClaimsHandler>();
            services.AddScoped<IAuthorizationHandler, WokkieBoxAuthorizationHandler>();
            services.AddScoped<IAuthorizationHandler, SecuriityLevelHandler>();
            services.AddScoped<IClaimsTransformation, ClaimsTransformation>();
            services.AddSingleton<IAuthorizationPolicyProvider, CustomAuthorizationPolicyProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }



            app.UseRouting();

            //who you are?

            app.UseAuthentication();

            //are you allowed?
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
