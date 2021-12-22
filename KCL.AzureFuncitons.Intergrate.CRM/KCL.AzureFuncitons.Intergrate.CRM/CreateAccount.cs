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
            log.LogInformation("C# HTTP trigger function processed a request.");

            //Build the connection string
            var clientId = "";
            var clientSecret = "";
            var organizationUrl = "https://org.crm.dynamics.com/";

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
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            id = id ?? data?.Id;

            string responseMessage = string.IsNullOrEmpty(id)
                ? "Pass an account id in the query string or in the request body for a personalized response."
                : $"Account Id, {id}. Account Name is " + GetAccountName(dataVerseConnection, id);

            return new OkObjectResult(responseMessage);
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