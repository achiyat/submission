using PromoIt.Entities;
using PromoIt.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoIt.Entitis.Commnd
{
    public class chooseCampaigns : ICommand
    {
        object ICommand.ExecuteCommand(params object[] param)
        {

            ICommand command = MainManager.Instance.commandManager.CommandList["chooseCampaigns"];
            CampaignActivist campaignActivist = new CampaignActivist();
            string Query;

            string requestBody = (string)param[0];
            string VarCampaignActivist = (string)param[5];

            try
            {
                MainManager.Instance.logger.Event("Activist/chooseCampaigns : insert new campaign Activist");
                campaignActivist = System.Text.Json.JsonSerializer.Deserialize<CampaignActivist>(requestBody);
                Query = $"insert into campaignActivist values({VarCampaignActivist})";
                MainManager.Instance.CampaignActivists.ExportFromDB(Query, campaignActivist);
                return null;
            }
            catch (Exception ex)
            {
                MainManager.Instance.logger.Exception($"Activist/chooseCampaigns : {ex.Message}", ex);
                return ex;
            }

            
        }
    }
}
