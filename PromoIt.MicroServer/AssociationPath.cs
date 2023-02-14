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
using static System.Collections.Specialized.BitVector32;
using PromoIt.Entitis.Commnd;
using PromoIt.Model;
using System.Collections;

namespace PromoIt.MicroServer
{
    public static class AssociationPath
    {
        [FunctionName("AssociationPath")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", "put", "delete", Route = "Association/{page?}/{action}/{IdNumber?}/{IdNumber1?}")] HttpRequest req, string page, string action, string IdNumber, string IdNumber1, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string VarAssociation = "@IDassn,@NameAssn,@EmailAssn";
            string VarCampaignAsso = $"@NameCampaign,{VarAssociation},@Fundraising,@linkURL,@Hashtag,@SelectedCampaign,@StatusCampaign";

            string requestBody;
            string responseMessage;


            ICommand command = MainManager.Instance.commandManager.CommandList[action];

            if (command != null)
            {
                requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                return new OkObjectResult(command.ExecuteCommand(requestBody,VarAssociation, VarCampaignAsso, IdNumber, IdNumber1));
            }
            else
            {
                MainManager.Instance.logger.Error($"Failed Request in AssociationPath");
                return new BadRequestObjectResult("Failed Request");
            }

        }
    }
}
