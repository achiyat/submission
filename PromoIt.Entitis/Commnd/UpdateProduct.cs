using PromoIt.Entities;
using PromoIt.Model;
using System.Collections;
using System;

namespace PromoIt.Entitis.Commnd
{
    public class UpdateProduct : ICommand
    {
        public object ExecuteCommand(params object[] param)
        {
            ICommand command = MainManager.Instance.commandManager.CommandList["UpdateProduct"];
            DonatedProduct product = new DonatedProduct();
            string Query;

            string requestBody = (string)param[0];

            try
            {
                MainManager.Instance.logger.Event($"Company/UpdateProduct : Update Product");
                product = System.Text.Json.JsonSerializer.Deserialize<DonatedProduct>(requestBody);
                string SetVar = "ProductName=@ProductName,Price=@Price,Inventory=@Inventory,StatusProduct=@StatusProduct where IDProduct = @IDProduct";
                Query = $"update DonatedProducts set {SetVar} \r\n update Shipments set {SetVar}";
                MainManager.Instance.DonatedProducts.ExportFromDB(Query, product);
                return null;
            }
            catch (Exception ex)
            {
                MainManager.Instance.logger.Exception($"Company/UpdateProduct : {ex.Message}", ex);
                return ex;
            }
        }
    }
} 