using PromoIt.Entities;
using PromoIt.Model;
using System.Collections;
using System;

namespace PromoIt.Entitis.Commnd
{
    public class GetCampaignsActivist : ICommand
    {
        public object ExecuteCommand(params object[] param)
        {
            ICommand command = MainManager.Instance.commandManager.CommandList["GetCampaignsActivist"];
            Hashtable hash;

            string IdNumber = (string)param[1];
            string responseMessage;

            try
            {
                MainManager.Instance.logger.Event($"User/GetCampaignsActivist : Get Campaigns Activist");
                hash = (Hashtable)MainManager.Instance.CampaignActivists.ImportData("select * from campaignActivist where IDactivist=" + IdNumber);
                responseMessage = System.Text.Json.JsonSerializer.Serialize(hash);
                return responseMessage;
            }
            catch (Exception ex)
            {
                MainManager.Instance.logger.Exception($"User/GetCampaignsActivis : {ex.Message}", ex);
                return ex;
            }
        }
    }
} 