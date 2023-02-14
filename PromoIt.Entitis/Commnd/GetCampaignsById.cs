using PromoIt.Entities;
using PromoIt.Model;
using System.Collections;
using System;

namespace PromoIt.Entitis.Commnd
{
    public class GetCampaignsById : ICommand
    {
        public object ExecuteCommand(params object[] param)
        {
            ICommand command = MainManager.Instance.commandManager.CommandList["GetCampaignsById"];
            Hashtable hash;

            string IdNumber = (string)param[1];
            string responseMessage;

            try
            {
                MainManager.Instance.logger.Event($"User/GetCampaignsById : Get Campaigns By Id");
                hash = (Hashtable)MainManager.Instance.CampaignsAsso.ImportData("select * from campaignAsso where IDassn = " + IdNumber);
                responseMessage = System.Text.Json.JsonSerializer.Serialize(hash);
                return responseMessage;
            }
            catch (Exception ex)
            {
                MainManager.Instance.logger.Exception($"User/GetCampaignsById : {ex.Message}", ex);
                return ex;
            }
        }
    }
} 