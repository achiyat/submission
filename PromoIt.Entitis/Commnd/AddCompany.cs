using PromoIt.Entities;
using PromoIt.Model;
using System;
using System.Collections;

namespace PromoIt.Entitis.Commnd
{
    public class AddCompany : ICommand
    {
        public object ExecuteCommand(params object[] param)
        {
            ICommand command = MainManager.Instance.commandManager.CommandList["AddCompany"];
            Company company = new Company();
            string Query;

            string requestBody = (string)param[0];
            string VarCompany = (string)param[1];

            try
            {
                MainManager.Instance.logger.Event($"Company/AddCompany : insert new Company");
                company = System.Text.Json.JsonSerializer.Deserialize<Company>(requestBody);
                Query = $"insert into companies values({VarCompany})";
                MainManager.Instance.Companies.ExportFromDB(Query, company);
                return null;
            }
            catch (Exception ex)
            {
                MainManager.Instance.logger.Exception($"Company/AddCompany : {ex.Message}", ex);
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
            Activist activist = new Activist();
            string Query;

            string requestBody = (string)param[0];
            string VarCompany = (string)param[1];
            string VarAssociation = (string)param[2];
            string VarCampaignAsso = (string)param[3];
            string VarCampaignCompany = (string)param[4];
            string VarProduct = (string)param[5];
            string IdNumber = (string)param[6];
            string IdNumber1 = (string)param[7];
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