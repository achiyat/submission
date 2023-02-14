using PromoIt.Entities;
using PromoIt.Model;
using System.Collections;
using System;

namespace PromoIt.Entitis.Commnd
{
    public class DeleteCampaign : ICommand
    {
        public object ExecuteCommand(params object[] param)
        {
            ICommand command = MainManager.Instance.commandManager.CommandList["DeleteCampaign"];
            CampaignOfAsso campaignAsso = new CampaignOfAsso();
            string Query;

            string requestBody = (string)param[0];

            try
            {
                MainManager.Instance.logger.Event($"Association/DeleteCampaign : delete campaign Association");
                campaignAsso = System.Text.Json.JsonSerializer.Deserialize<CampaignOfAsso>(requestBody);
                Query = "delete from campaignAsso where IDcampaign = @IDcampaign";
                MainManager.Instance.CampaignsAsso.ExportFromDB(Query, campaignAsso);
                return null;
            }
            catch (Exception ex)
            {
                MainManager.Instance.logger.Exception($"Association/DeleteCampaign : {ex.Message}", ex);
                return ex;
            }
        }
    }
} 