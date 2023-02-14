using PromoIt.Entities;
using PromoIt.Model;
using System.Collections;
using System;

namespace PromoIt.Entitis.Commnd
{
    public class GetCompanyById : ICommand
    {
        public object ExecuteCommand(params object[] param)
        {
            ICommand command = MainManager.Instance.commandManager.CommandList["GetCompanyById"];
            Hashtable hash;
            Company company = new Company();
            string Query;

            string IdNumber = (string)param[6];
            string responseMessage;

            try
            {
                MainManager.Instance.logger.Event($"GetCompanyById : Get CompanyById");
                hash = (Hashtable)MainManager.Instance.Companies.ImportData("select * from companies");
                company = (Company)hash[IdNumber]; //email
                responseMessage = System.Text.Json.JsonSerializer.Serialize(company);
                return responseMessage;
            }
            catch (Exception ex)
            {
                MainManager.Instance.logger.Exception($"GetCompanyById : {ex.Message}", ex);
                return ex;
            }
        }
    }
} 