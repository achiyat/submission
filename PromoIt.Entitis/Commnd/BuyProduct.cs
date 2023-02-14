using PromoIt.Entities;
using PromoIt.Model;
using System;

namespace PromoIt.Entitis.Commnd
{
    public class BuyProduct : ICommand
    {
        public object ExecuteCommand(params object[] param)
        {
            ICommand command = MainManager.Instance.commandManager.CommandList["BuyProduct"];
            Shipping shipping = new Shipping();
            string Query;

            string requestBody = (string)param[0];

            try
            {
                MainManager.Instance.logger.Event("Activist/BuyProduct : Updates the queries that the item was bought or donated");
                shipping = System.Text.Json.JsonSerializer.Deserialize<Shipping>(requestBody);
                Query = "update Shipments set bought=@bought,donated=@donated where IDShipments = @IDShipments\r\n update Shipments set Inventory=@Inventory where IDProduct = @IDProduct\r\n update Shipments set Fundraising=@Fundraising where IDactivist = @IDactivist and IDcampaign = @IDcampaign \r\n update Shipments set MoneyActivist=@MoneyActivist,MoneySpent=@MoneySpent where IDactivist = @IDactivist\r\n update DonatedProducts set Inventory=@Inventory,Fundraising=@Fundraising where IDcampaign = @IDcampaign\r\n update campaignCompany set Fundraising=@Fundraising where IDcampaign = @IDcampaign\r\n update campaignAsso set Fundraising=@Fundraising where IDcampaign = @IDcampaign\r\n update campaignActivist set Fundraising=@Fundraising where IDactivist = @IDactivist and IDcampaign = @IDcampaign\r\n update campaignActivist set MoneyActivist=@MoneyActivist,MoneySpent=@MoneySpent where IDactivist = @IDactivist";
                MainManager.Instance.Shipments.ExportFromDB(Query, shipping);
                return null;
            }
            catch (Exception ex)
            {
                MainManager.Instance.logger.Exception($"Activist/BuyProduct : {ex.Message}", ex);
                return ex;
            }
        }
    }
}