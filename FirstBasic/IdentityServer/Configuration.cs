using IdentityModel;
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
        public static IEnumerable<ApiResource> GetApis() =>
            new List<ApiResource> {
                new ApiResource ("ApiOne"),
                new ApiResource ("ApiTwo"),
            };


        public static IEnumerable<ApiScope> Scopes
        {
            get
            {
                return new List<ApiScope> { new ApiScope("ApiOne")};
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

                AllowedScopes = {"ApiOne", "ApiTwo"}
            }
            };
    }
}
