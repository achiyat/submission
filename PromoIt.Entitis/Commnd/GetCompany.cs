using PromoIt.Entities;
using PromoIt.Model;
using System.Collections;
using System;

namespace PromoIt.Entitis.Commnd
{
    public class GetCompany : ICommand
    {
        public object ExecuteCommand(params object[] param)
        {
            ICommand command = MainManager.Instance.commandManager.CommandList["GetCompany"];
            Hashtable hash;
            string Query;

            string responseMessage;

            try
            {
                MainManager.Instance.logger.Event($"GetCompany : Get Company");
                hash = (Hashtable)MainManager.Instance.Companies.ImportData("select * from companies");
                responseMessage = System.Text.Json.JsonSerializer.Serialize(hash);
                return responseMessage;
            }
            catch (Exception ex)
            {
                MainManager.Instance.logger.Exception($"GetCompany : {ex.Message}", ex);
                return ex;
            }
        }
    }
} 