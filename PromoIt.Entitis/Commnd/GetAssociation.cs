using PromoIt.Entities;
using PromoIt.Model;
using System.Collections;
using System;

namespace PromoIt.Entitis.Commnd
{
    public class GetAssociation : ICommand
    {
        public object ExecuteCommand(params object[] param)
        {
            ICommand command = MainManager.Instance.commandManager.CommandList["GetAssociation"];
            Hashtable hash;
            string Query;

            string responseMessage;

            try
            {
                MainManager.Instance.logger.Event($"Association/GetAssociation : Get Associations");
                hash = (Hashtable)MainManager.Instance.Associations.ImportData("select * from Associations");
                responseMessage = System.Text.Json.JsonSerializer.Serialize(hash);
                return responseMessage;
            }
            catch (Exception ex)
            {
                MainManager.Instance.logger.Exception($"Association/GetAssociation : {ex.Message}", ex);
                return ex;
            }
        }
    }
} 