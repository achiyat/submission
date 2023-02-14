using PromoIt.Entities;
using PromoIt.Model;
using System.Collections;
using System;

namespace PromoIt.Entitis.Commnd
{
    public class UpdateCampaign : ICommand
    {
        public object ExecuteCommand(params object[] param)
        {
            ICommand command = MainManager.Instance.commandManager.CommandList["UpdateCampaign"];
            CampaignOfAsso campaignAsso = new CampaignOfAsso();
            string Query;

            string requestBody = (string)param[0];

            try
            {
                MainManager.Instance.logger.Event($"Association/UpdateCampaign : Update campaign Association");
                campaignAsso = System.Text.Json.JsonSerializer.Deserialize<CampaignOfAsso>(requestBody);
                string SetVar = "NameCampaign=@NameCampaign,linkURL=@linkURL,Hashtag=@Hashtag,SelectedCampaign=@SelectedCampaign,StatusCampaign=@StatusCampaign where IDcampaign = @IDcampaign";
                Query = $"update campaignAsso set {SetVar} \r\n update campaignCompany set {SetVar} \r\n update DonatedProducts set {SetVar} \r\n update campaignActivist set {SetVar} \r\n update Shipments set {SetVar}";
                MainManager.Instance.CampaignsAsso.ExportFromDB(Query, campaignAsso);
                return null;
            }
            catch (Exception ex)
            {
                MainManager.Instance.logger.Exception($"Association/UpdateCampaign : {ex.Message}", ex);
                return ex;
            }
        }
    }
} 