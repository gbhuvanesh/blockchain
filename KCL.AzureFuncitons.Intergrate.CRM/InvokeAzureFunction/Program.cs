using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Microsoft.Identity.Client.Extensions.Msal;

namespace InvokeAzureFunction
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1. Configuration - read below about redirect URI
            var pca = PublicClientApplicationBuilder.Create("client_id")
            .WithBroker()
            .Build();
            // Add a token cache, see https://docs.microsoft.com/en-us/azure/active-directory/develop/msal-net-tokencache-
            serialization? tabs = desktop
// 2. GetAccounts
var accounts = await pca.GetAccountsAsync();
            var accountToLogin = // choose an account, or null, or use PublicClientApplication.OperatingSystemAccount
for the default OS account
try
                {
                    // 3. AcquireTokenSilent
                    var authResult = await pca.AcquireTokenSilent(new[] { "User.Read" }, accountToLogin)
                    .ExecuteAsync();
                }
                catch (MsalUiRequiredException) // no change in the pattern
                {
                    // 4. Specific: Switch to the UI thread for next call . Not required for console apps.
                    await SwitchToUiThreadAsync(); // not actual code, this is different on each platform / tech
                                                   // 5. AcquireTokenInteractive
                    var authResult = await pca.AcquireTokenInteractive(new[] { "User.Read" })
                    .WithAccount(accountToLogin) // this already exists in MSAL, but it is more important for WAM
                    .WithParentActivityOrWindow(myWindowHandle) // to be able to parent WAM's windows to your app (optional, but
highly recommended; not needed on UWP)
.ExecuteAsync();
        }

        var clientCredential = new ClientCredential("bad0fff4-629c-47e1-9abd-359db29e35c0", "m257Q~FU0t3jh8NLAcjlPqhfpguJ4It1BrMBD");
            AuthenticationContext context = new AuthenticationContext("https://login.microsoftonline.com/b755419a-8313-4815-baf9-a96e7ef5b969/oauth2/v2.0/authorize", false);
            AuthenticationResult authResult = context.AcquireTokenAsync(
                "bad0fff4-629c-47e1-9abd-359db29e35c0",
                clientCredential).Result;

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authResult.AccessToken);
            var response = client.GetAsync("https://tonytestad.azurewebsites.net/api/HttpTrigger1?code=LoyOi4C&name=123").Result;
            response.EnsureSuccessStatusCode();
        }
    }
}
