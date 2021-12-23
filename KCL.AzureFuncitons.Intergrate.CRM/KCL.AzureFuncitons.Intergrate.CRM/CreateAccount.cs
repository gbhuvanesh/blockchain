using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;

namespace KCL.AzureFuncitons.Intergrate.CRM
{
    internal class RetrieveAccount
    {

        [FunctionName("RetrieveAccount")]
        public static async Task<IActionResult> Run(
                  [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
                  ILogger log)
        {
            try
            {
                log.LogInformation("C# HTTP trigger function processed a request.");

                //throw new Exception("exception log test");

                //Build the connection string
                var clientId = "3fe639dd-9f2c-409b-823d-f5928b9b59a4";
                var clientSecret = "h3V7Q~.lCtjofjufuEGk--0seSP14DUxAfF4c";
                var organizationUrl = "https://bgprod.crm11.dynamics.com";

                string connectionString = "Url=" + organizationUrl + "; " +
                       "AuthType=ClientSecret; " +
                       "ClientId= " + clientId + "; " +
                       "ClientSecret=" + clientSecret + "; " +
                       "RequireNewInstance=false; " +
                       "SkipDiscovery=true";

                ServiceClient dataVerseConnection = new ServiceClient(connectionString);
                if (!dataVerseConnection.IsReady)
                {
                    throw new Exception("Authentication Failed!");
                }

                string id = req.Query["Id"];
                //string id = "3bcfb4b4-4213-ec11-b6e5-002248412473";
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                dynamic data = JsonConvert.DeserializeObject(requestBody);
                id = id ?? data?.Id;

                string responseMessage = string.IsNullOrEmpty(id)
                    ? "Pass an account id in the query string or in the request body for a personalized response."
                    : $"Account Id, {id}. Account Name is " + GetAccountName(dataVerseConnection, id);

                return new OkObjectResult(responseMessage);

            }
            catch (Exception ex)
            {
                log.LogInformation(ex.Message);
            }
            return null;
        }


        public static string GetAccountName(ServiceClient dataVerseConnection, string accountId)
        {
            ColumnSet attributes = new ColumnSet(new string[] { "name" });
            Entity accountEntity = dataVerseConnection.Retrieve("account", new Guid(accountId), attributes);
            string accountName = "";
            if (accountEntity != null)
            {
                accountName = (accountEntity.Attributes.Contains("name")) ? accountEntity.GetAttributeValue<string>("name") : string.Empty;

            }
            return accountName;
        }
    }
}