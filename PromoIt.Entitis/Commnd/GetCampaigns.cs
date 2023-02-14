using PromoIt.Entities;
using PromoIt.Model;
using System.Collections;
using System;

namespace PromoIt.Entitis.Commnd
{
    public class GetCampaigns : ICommand
    {
        public object ExecuteCommand(params object[] param)
        {
            ICommand command = MainManager.Instance.commandManager.CommandList["GetCampaigns"];
            Hashtable hash;

            string responseMessage;

            try
            {
                MainManager.Instance.logger.Event($"User/GetCampaigns : Get Campaigns");
                hash = (Hashtable)MainManager.Instance.CampaignsAsso.ImportData("select * from campaignAsso");
                responseMessage = System.Text.Json.JsonSerializer.Serialize(hash);
                return responseMessage;
            }
            catch (Exception ex)
            {
                MainManager.Instance.logger.Exception($"User/GetCampaigns : {ex.Message}", ex);
                return ex;
            }
        }
    }
} 