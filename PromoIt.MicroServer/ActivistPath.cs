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
using PromoIt.Entitis.Commnd;

namespace PromoIt.MicroServer
{
    public static class ActivistPath
    {
        [FunctionName("ActivistPath")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", "put", "delete", Route = "Activist/{page?}/{action}/{IdNumber?}/{IdNumber1?}")] HttpRequest req, string page, string action, string IdNumber, string IdNumber1, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            Hashtable hash;
            Activist Activ = new Activist();
            CampaignActivist campaignActivist = new CampaignActivist();
            Shipping shipping = new Shipping();

            string VarActivist = "@IDactivist,@NameActivist,@EmailActivist,@AddressActivist,@phoneActivist,@NameUserTweeter";
            string VarCompany = "@IDCompany,@NameCompany,@OwnerCompany,@EmailCompany,@PhoneCompany";
            string VarAssociation = "@IDassn,@NameAssn,@EmailAssn";

            string VarCampaignAsso = $"@NameCampaign,{VarAssociation},@Fundraising,@linkURL,@Hashtag,@SelectedCampaign,@StatusCampaign";
            string VarCampaignActivist = $"@IDcampaign,{VarCampaignAsso},{VarActivist},@MoneyActivist,@MoneySpent";
            string VarCampaignCompany = $"@IDcampaign,{VarCampaignAsso},{VarCompany}";

            string VarProduct = $"@ProductName,@Price,@Inventory,@SelectedProduct,@StatusProduct,{VarCampaignCompany}";
            string VarShipping = $"@donated,@bought,@IDProduct,{VarProduct},{VarActivist},@MoneyActivist,@MoneySpent";


            string Query;
            string responseMessage;
            string requestBody;

            ICommand command = MainManager.Instance.commandManager.CommandList[action];


            if (command != null)
            {
                requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                return new OkObjectResult(command.ExecuteCommand(requestBody, VarActivist, VarCompany, VarAssociation, VarCampaignAsso, VarCampaignActivist, VarCampaignCompany, VarProduct, VarShipping, IdNumber, IdNumber1));
            }
            else
            {
                MainManager.Instance.logger.Error($"Failed Request in ActivistPath");
                return new BadRequestObjectResult("Failed Request");
            }

        }
    }
}

/*
             string requestBody = (string)param[0];
            string VarActivist = (string)param[1];
            string VarCompany = (string)param[2];
            string VarAssociation = (string)param[3];
            string VarCampaignAsso = (string)param[4];
            string VarCampaignActivist = (string)param[5];
            string VarCampaignCompany = (string)param[6];
            string VarProduct = (string)param[7];
            string VarShipping = (string)param[8];
            string IdNumber = (string)param[9];
            string IdNumber1 = (string)param[10];
 */