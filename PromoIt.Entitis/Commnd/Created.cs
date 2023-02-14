using PromoIt.Entities;
using PromoIt.Model;
using System.Collections;
using System;

namespace PromoIt.Entitis.Commnd
{
    public class Created : ICommand
    {
        public object ExecuteCommand(params object[] param)
        {
            ICommand command = MainManager.Instance.commandManager.CommandList["Created"];
            CampaignOfAsso campaignAsso = new CampaignOfAsso();
            string Query;

            string requestBody = (string)param[0];
            string VarCampaignAsso = (string)param[2];

            try
            {
                MainManager.Instance.logger.Event($"Association/Created : insert new campaign Association");
                campaignAsso = System.Text.Json.JsonSerializer.Deserialize<CampaignOfAsso>(requestBody);
                Query = $"insert into campaignAsso values({VarCampaignAsso})";
                MainManager.Instance.CampaignsAsso.ExportFromDB(Query, campaignAsso);
                return null;
            }
            catch (Exception ex)
            {
                MainManager.Instance.logger.Exception($"Association/Created : {ex.Message}", ex);
                return ex;
            }
        }
    }
} 