using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer
{
    public static class Configuration
    {
        /// <summary>
        /// register open id as resource thst csn be accessed
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IdentityResource> GetIdentityResources() =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
                //new IdentityResources.Email();

            };

        public static IEnumerable<ApiResource> GetApis() =>
            new List<ApiResource> {
                new ApiResource ("ApiOne"),
                new ApiResource ("ApiTwo"),
            };


        public static IEnumerable<ApiScope> Scopes
        {
            get
            {
                return new List<ApiScope> { new ApiScope("ApiOne"), new ApiScope("ApiTwo") };
            }
        }

        public static IEnumerable<Client> GeClients() =>
            new List<Client>
            {
            new Client
            {
                ClientId = "client_id",
                ClientSecrets = { new Secret("client_secret".ToSha256())},

                AllowedGrantTypes = GrantTypes.ClientCredentials,

                AllowedScopes = {"ApiOne"}
            },
            new Client
            {
                ClientId = "client_id_mvc",
                ClientSecrets = { new Secret("client_secret".ToSha256())},

                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris = { "https://localhost:44389/signin-oidc" },
                AllowedScopes = {"ApiOne", "ApiTwo", 
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                    }
            }
            };
    }
}
