using PromoIt.Entities;
using PromoIt.Model;
using System.Collections;
using System;

namespace PromoIt.Entitis.Commnd
{
    public class DeleteProduct : ICommand
    {
        public object ExecuteCommand(params object[] param)
        {
            ICommand command = MainManager.Instance.commandManager.CommandList["DeleteProduct"];
            DonatedProduct product = new DonatedProduct();
            string Query;

            string requestBody = (string)param[0];

            try
            {
                MainManager.Instance.logger.Event($"Company/DeleteProduct : delete Product");
                product = System.Text.Json.JsonSerializer.Deserialize<DonatedProduct>(requestBody);
                Query = "delete from DonatedProducts where IDProduct = @IDProduct\r\n delete from Shipments where IDProduct = @IDProduct";
                MainManager.Instance.DonatedProducts.ExportFromDB(Query, product);
                return null;
            }
            catch (Exception ex)
            {
                MainManager.Instance.logger.Exception($"Company/DeleteProduct : {ex.Message}", ex);
                return ex;
            }
        }
    }
} 