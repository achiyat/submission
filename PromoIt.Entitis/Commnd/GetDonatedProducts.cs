using PromoIt.Entities;
using PromoIt.Model;
using System.Collections;
using System;

namespace PromoIt.Entitis.Commnd
{
    public class GetDonatedProducts : ICommand
    {
        public object ExecuteCommand(params object[] param)
        {
            ICommand command = MainManager.Instance.commandManager.CommandList["GetDonatedProducts"];
            Hashtable hash;

            string responseMessage;

            try
            {
                MainManager.Instance.logger.Event($"User/GetDonatedProducts : Get Donated Products");
                hash = (Hashtable)MainManager.Instance.DonatedProducts.ImportData("select * from DonatedProducts");
                responseMessage = System.Text.Json.JsonSerializer.Serialize(hash);
                return responseMessage;
            }
            catch (Exception ex)
            {
                MainManager.Instance.logger.Exception($"User/GetDonatedProducts : {ex.Message}", ex);
                return ex;
            }
        }
    }
} 