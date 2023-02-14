using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PromoIt.Entities;
using PromoIt.Model;
using System.Collections;
using Newtonsoft.Json.Linq;
using RestSharp;
using Tweetinvi;
using PromoIt.Entitis.Commnd;

namespace PromoIt.MicroServer
{
    public static class UserPath
    {
        [FunctionName("UserPath")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", "put", "delete", Route = "User/{page?}/{action}/{IdNumber?}/{IdNumber1?}")] HttpRequest req, string page, string action, string IdNumber, string IdNumber1, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            Hashtable hash;
            Shipping shipping = new Shipping();

            string responseMessage;
            string requestBody;
            string Query;

            ICommand command = MainManager.Instance.commandManager.CommandList[action];

            if (command != null)
            {
                requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                return new OkObjectResult(command.ExecuteCommand(requestBody, IdNumber, IdNumber1));
            }
            else
            {
                MainManager.Instance.logger.Error($"Failed Request in UserPath");
                return new BadRequestObjectResult("Failed Request");
            }

        }
    }
}
