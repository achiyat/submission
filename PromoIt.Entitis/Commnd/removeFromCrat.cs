using PromoIt.Entities;
using PromoIt.Model;
using System;

namespace PromoIt.Entitis.Commnd
{
    public class removeFromCrat : ICommand
    {
        public object ExecuteCommand(params object[] param)
        {
            ICommand command = MainManager.Instance.commandManager.CommandList["removeFromCrat"];
            Shipping shipping = new Shipping();
            string Query;

            string requestBody = (string)param[0];

            try
            {
                MainManager.Instance.logger.Event("Activist/removeFromCrat : delete from Shipments");
                shipping = System.Text.Json.JsonSerializer.Deserialize<Shipping>(requestBody);
                Query = "delete from Shipments where IDShipments = @IDShipments";
                MainManager.Instance.Shipments.ExportFromDB(Query, shipping);
                return null;
            }
            catch (Exception ex)
            {
                MainManager.Instance.logger.Exception($"Activist/removeFromCrat : {ex.Message}", ex);
                return ex;
            }
        }
    }
}