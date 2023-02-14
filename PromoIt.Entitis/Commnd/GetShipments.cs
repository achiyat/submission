using PromoIt.Entities;
using PromoIt.Model;
using System.Collections;
using System;

namespace PromoIt.Entitis.Commnd
{
    public class GetShipments : ICommand
    {
        public object ExecuteCommand(params object[] param)
        {
            ICommand command = MainManager.Instance.commandManager.CommandList["GetShipments"];
            Hashtable hash;

            string responseMessage;

            try
            {
                MainManager.Instance.logger.Event($"User/GetShipments : Get Shipments");
                hash = (Hashtable)MainManager.Instance.Shipments.ImportData("select * from Shipments");
                responseMessage = System.Text.Json.JsonSerializer.Serialize(hash);
                return responseMessage;
            }
            catch (Exception ex)
            {
                MainManager.Instance.logger.Exception($"User/GetShipments : {ex.Message}", ex);
                return ex;
            }
        }
    }
} 