using PromoIt.Entities;
using PromoIt.Model;
using System.Collections;
using System;

namespace PromoIt.Entitis.Commnd
{
    public class GetProductActivist : ICommand
    {
        public object ExecuteCommand(params object[] param)
        {
            ICommand command = MainManager.Instance.commandManager.CommandList["GetProductActivist"];
            Hashtable hash;

            string IdNumber = (string)param[1];
            string IdNumber1 = (string)param[2];
            string responseMessage;

            try
            {
                MainManager.Instance.logger.Event($"User/GetProductActivist : Get Product Activist");
                hash = (Hashtable)MainManager.Instance.InnerJoins.ImportData("select * from DonatedProducts d inner join campaignActivist c on c.IDcampaign = d.IDcampaign where c.IDactivist=" + IdNumber + "and c.IDcampaign=" + IdNumber1 + "and d.StatusProduct=1 and d.StatusCampaign=1");
                responseMessage = System.Text.Json.JsonSerializer.Serialize(hash);
                return responseMessage;
            }
            catch (Exception ex)
            {
                MainManager.Instance.logger.Exception($"User/GetProductActivist : {ex.Message}", ex);
                return ex;
            }
        }
    }
} 