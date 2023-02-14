using PromoIt.Entities;
using PromoIt.Model;
using System.Collections;
using System;

namespace PromoIt.Entitis.Commnd
{
    public class GetCampaignsCompany : ICommand
    {
        public object ExecuteCommand(params object[] param)
        {
            ICommand command = MainManager.Instance.commandManager.CommandList["GetCampaignsCompany"];
            Hashtable hash;

            string IdNumber = (string)param[1];
            string responseMessage;

            try
            {
                MainManager.Instance.logger.Event($"User/GetCampaignsCompany : Get Campaigns Company");
                hash = (Hashtable)MainManager.Instance.CampaignCompany.ImportData("select * from campaignCompany where IDCompany=" + IdNumber);
                responseMessage = System.Text.Json.JsonSerializer.Serialize(hash);
                return responseMessage;
            }
            catch (Exception ex)
            {
                MainManager.Instance.logger.Exception($"User/GetCampaignsCompany : {ex.Message}", ex);
                return ex;
            }
        }
    }
} 