using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InvokeAzureFunction
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var clientCredential = new ClientCredential("{app_client_id}", "{app_client_secret}");
            AuthenticationContext context = new AuthenticationContext("https://login.microsoftonline.com/xx.onmicrosoft.com", false);
            AuthenticationResult authResult = context.AcquireTokenAsync(
                "{app_client_id}",
                clientCredential).Result;

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authResult.AccessToken);
            var response = client.GetAsync("https://tonytestad.azurewebsites.net/api/HttpTrigger1?code=LoyOi4C&name=123").Result;
            response.EnsureSuccessStatusCode();
        }
    }
}
