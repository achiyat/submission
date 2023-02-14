using PromoIt.Entities;
using System;
using System.Collections;
using System.Security.Policy;

namespace PromoIt.Entitis.Commnd
{
    public class GetActivist : ICommand
    {
        public object ExecuteCommand(params object[] param)
        {
            ICommand command = MainManager.Instance.commandManager.CommandList["GetActivist"];
            Hashtable hash;
            string Query;
            string responseMessage;

            try
            {
                MainManager.Instance.logger.Event("Activist/GetActivist : Get Activists");
                hash = (Hashtable)MainManager.Instance.Activists.ImportData("select * from Activists");
                responseMessage = System.Text.Json.JsonSerializer.Serialize(hash);
                return responseMessage;
            }
            catch (Exception ex)
            {
                MainManager.Instance.logger.Exception($"Activist/GetActivist : {ex.Message}", ex);
                return ex;
            }
        }
    }
}