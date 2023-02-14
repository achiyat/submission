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
using static System.Collections.Specialized.BitVector32;
using System.Collections;
using PromoIt.Entitis.Commnd;

namespace PromoIt.MicroServer
{
    public static class CompanyPath
    {
        [FunctionName("CompanyPath")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", "put", "delete", Route = "Company/{page?}/{action}/{IdNumber?}/{IdNumber1?}")] HttpRequest req, string page, string action, string IdNumber, string IdNumber1, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            Hashtable hash;
            Company company = new Company();
            DonatedProduct product = new DonatedProduct();

            string VarCompany = "@IDCompany,@NameCompany,@OwnerCompany,@EmailCompany,@PhoneCompany";
            string VarAssociation = "@IDassn,@NameAssn,@EmailAssn";

            string VarCampaignAsso = $"@NameCampaign,{VarAssociation},@Fundraising,@linkURL,@Hashtag,@SelectedCampaign,@StatusCampaign";
            string VarCampaignCompany = $"@IDcampaign,{VarCampaignAsso},{VarCompany}";

            string VarProduct = $"@ProductName,@Price,@Inventory,@SelectedProduct,@StatusProduct,{VarCampaignCompany}";

            string responseMessage;
            string requestBody;
            string Query;

            ICommand command = MainManager.Instance.commandManager.CommandList[action];

            if (command != null)
            {
                requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                return new OkObjectResult(command.ExecuteCommand(requestBody, VarCompany, VarAssociation, VarCampaignAsso, VarCampaignCompany, VarProduct, IdNumber, IdNumber1));
            }
            else
            {
                MainManager.Instance.logger.Error($"Failed Request in CompanyPath");
                return new BadRequestObjectResult("Failed Request");
            }
        }
    }
}
