using PromoIt.Entities;
using PromoIt.Model;
using System.Collections;
using System;
using System.IO;

namespace PromoIt.Entitis.Commnd
{
    public class Upload : ICommand
    {
        public object ExecuteCommand(params object[] param)
        {
            ICommand command = MainManager.Instance.commandManager.CommandList["Upload"];
            DonatedProduct product = new DonatedProduct();
            string Query;

            string requestBody = (string)param[0];
            string VarCampaignCompany = (string)param[4];
            string VarProduct = (string)param[5];

            try
            {
                MainManager.Instance.logger.Event($"Company/Upload : insert new Product into DonatedProducts");
                product = System.Text.Json.JsonSerializer.Deserialize<DonatedProduct>(requestBody);
                string SetVar = "NameCampaign=@NameCampaign,linkURL=@linkURL,Hashtag=@Hashtag,SelectedCampaign=@SelectedCampaign where IDcampaign = @IDcampaign";
                Query = $"insert into DonatedProducts values({VarProduct}) \r\n insert into campaignCompany values({VarCampaignCompany}) \r\n update campaignAsso set {SetVar} \r\n update campaignCompany set {SetVar} \r\n update DonatedProducts set {SetVar} \r\n update campaignActivist set {SetVar} \r\n update Shipments set {SetVar}";
                MainManager.Instance.DonatedProducts.ExportFromDB(Query, product);
                return null;
            }
            catch (Exception ex)
            {
                MainManager.Instance.logger.Exception($"Company/Upload : {ex.Message}", ex);
                return ex;
            }
        }
    }
}