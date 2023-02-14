using PromoIt.Entities;
using PromoIt.Model;
using System.Collections;
using System;

namespace PromoIt.Entitis.Commnd
{
    public class AddAssociation : ICommand
    {
        public object ExecuteCommand(params object[] param)
        {
            ICommand command = MainManager.Instance.commandManager.CommandList["AddAssociation"];
            Association association = new Association();
            string Query;

            string requestBody = (string)param[0];
            string VarAssociation = (string)param[1];

            try
            {
                MainManager.Instance.logger.Event($"Association/AddAssociation : insert new Association");
                association = System.Text.Json.JsonSerializer.Deserialize<Association>(requestBody);
                Query = $"insert into Associations values({VarAssociation})";
                MainManager.Instance.Associations.ExportFromDB(Query, association);
                return null;
            }
            catch (Exception ex)
            {
                MainManager.Instance.logger.Exception($"Association/AddAssociation : {ex.Message}", ex);
                return ex;
            }
        }
    }
}

/*
        public object ExecuteCommand(params object[] param)
        {
            ICommand command = MainManager.Instance.commandManager.CommandList["111111111111111"];
            Hashtable hash;
            Association Asso = new Association();
            CampaignOfAsso campaignAsso = new CampaignOfAsso();

            string requestBody;
            string Query;

            string VarAssociation = (string)param[0];
            string VarCampaignAsso = (string)param[1];
            string IdNumber = (string)param[2];
            string IdNumber1 = (string)param[3];
            string responseMessage;

            try
            {
                return null;
            }
            catch (Exception ex)
            {
                MainManager.Instance.logger.Event($"1111111111111111111");
                MainManager.Instance.logger.Exception($"1111111111111111111 : {ex.Message}", ex);
                return ex;
            }
        }
 */