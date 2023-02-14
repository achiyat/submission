using PromoIt.Entities;
using PromoIt.Model;
using System;
using System.IO;

namespace PromoIt.Entitis.Commnd
{
    public class AddShipments : ICommand
    {
        public object ExecuteCommand(params object[] param)
        {
            ICommand command = MainManager.Instance.commandManager.CommandList["AddShipments"];
            Shipping shipping = new Shipping();
            string Query;

            string requestBody = (string)param[0];
            string VarShipping = (string)param[8];

            try
            {
                MainManager.Instance.logger.Event("Activist/AddShipments : insert new Shipments");
                shipping = System.Text.Json.JsonSerializer.Deserialize<Shipping>(requestBody);
                Query = $"insert into Shipments values({VarShipping}) \r\n update DonatedProducts set SelectedProduct= @SelectedProduct where IDProduct = @IDProduct";
                MainManager.Instance.Shipments.ExportFromDB(Query, shipping);
                return null;
            }
            catch (Exception ex)
            {
                MainManager.Instance.logger.Exception($"Activist/AddShipments : {ex.Message}", ex);
                return ex;
            }
        }
    }
}

