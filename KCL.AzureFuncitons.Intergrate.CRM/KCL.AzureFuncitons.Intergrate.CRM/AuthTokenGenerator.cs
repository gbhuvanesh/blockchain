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
using System.Net.Http;
using System.Linq;


namespace KCL.AzureFuncitons.Intergrate.CRM
{
    public class AuthTokenGenerator
    {
        //[FunctionName("AuthTokenGenerator")]
        //public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] Microsoft.AspNetCore.Http.HttpRequest req, ILogger log)
        //{
        //    //var code = req.
        //}
    }
}
