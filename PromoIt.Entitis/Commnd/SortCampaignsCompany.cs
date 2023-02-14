using PromoIt.Entities;
using PromoIt.Model;
using System.Collections;
using System;

namespace PromoIt.Entitis.Commnd
{
    public class SortCampaignsCompany : ICommand
    {
        public object ExecuteCommand(params object[] param)
        {
            ICommand command = MainManager.Instance.commandManager.CommandList["SortCampaignsCompany"];
            Hashtable hash;

            string IdNumber = (string)param[1];
            string responseMessage;

            try
            {
                MainManager.Instance.logger.Event($"User/SortCampaignsCompany : get Get a sorted list of company campaign");
                hash = (Hashtable)MainManager.Instance.CampaignCompany.ImportData("select * from campaignAsso c \r\n inner join campaignCompany ca\r\n on c.IDcampaign = ca.IDcampaign\r\n where ca.IDCompany =" + IdNumber);
                responseMessage = System.Text.Json.JsonSerializer.Serialize(hash);
                return responseMessage;
            }
            catch (Exception ex)
            {
                MainManager.Instance.logger.Exception($"User/SortCampaignsCompany : {ex.Message}", ex);
                return ex;
            }
        }
    }
}  