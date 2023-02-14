using PromoIt.Entities;
using PromoIt.Model;
using System.Collections;
using System;

namespace PromoIt.Entitis.Commnd
{
    public class GetAssociationById : ICommand
    {
        public object ExecuteCommand(params object[] param)
        {
            ICommand command = MainManager.Instance.commandManager.CommandList["GetAssociationById"];
            Hashtable hash;
            Association association = new Association();
            string Query;

            string IdNumber = (string)param[3];
            string responseMessage;

            try
            {
                MainManager.Instance.logger.Event($"Association/GetAssociationById : Get Association");
                hash = (Hashtable)MainManager.Instance.Associations.ImportData("select * from Associations");
                association = (Association)hash[IdNumber]; //email
                responseMessage = System.Text.Json.JsonSerializer.Serialize(association);
                return responseMessage;
            }
            catch (Exception ex)
            {
                MainManager.Instance.logger.Exception($"Association/GetAssociationById : {ex.Message}", ex);
                return ex;
            }
        }
    }
} 