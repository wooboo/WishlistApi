using System;
using IdentityModel.Client;
using Microsoft.Owin.Hosting;

namespace ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "http://localhost:8080/";

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                // Create HttpCient and make a request to api/values 
                //HttpClient client = new HttpClient();
                //var tokenResponse = RequestToken();
                //client.SetBearerToken(tokenResponse.AccessToken);
                //StringContent theContent = new StringContent("{'name':'hello world'}", System.Text.Encoding.UTF8, "application/json");
                //var response = client.PostAsync(baseAddress + "wishlist/api/ownerslist", theContent).Result;


                //response = client.GetAsync(baseAddress + "wishlist/api/ownerslist").Result;

                //Console.WriteLine(response);
                //Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                Console.ReadLine();
            }

        }
        static TokenResponse RequestToken()
        {
            var client = new TokenClient(
                Constants.TokenEndpoint,
                "roclient",
                "secret");

            //return client.RequestResourceOwnerPasswordAsync("wooboo","kop","read write").Result;
            return client.RequestResourceOwnerPasswordAsync("bubu","bubu","read write").Result;
        }

    }
    public static class Constants
    {
        public const string BaseAddress = "https://localhost:44344";

        public const string AuthorizeEndpoint = BaseAddress + "/connect/authorize";
        public const string LogoutEndpoint = BaseAddress + "/connect/endsession";
        public const string TokenEndpoint = BaseAddress + "/connect/token";
        public const string UserInfoEndpoint = BaseAddress + "/connect/userinfo";
        public const string IdentityTokenValidationEndpoint = BaseAddress + "/connect/identitytokenvalidation";
        public const string TokenRevocationEndpoint = BaseAddress + "/connect/revocation";

        public const string AspNetWebApiSampleApi = "http://localhost:2727/";
    }
}
