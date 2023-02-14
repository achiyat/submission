using PromoIt.Entities;
using PromoIt.Model;
using System;
using System.Collections;
using System.Security.Policy;

namespace PromoIt.Entitis.Commnd
{
    public class GetActivistById : ICommand
    {
        public object ExecuteCommand(params object[] param)
        {
            ICommand command = MainManager.Instance.commandManager.CommandList["GetActivistById"];
            Hashtable hash;
            Activist activist = new Activist();
            string responseMessage;
            string Query;

            string IdNumber = (string)param[9];


            try
            {
                MainManager.Instance.logger.Event("Activist/GetActivistById : Get Activist");
                hash = (Hashtable)MainManager.Instance.Activists.ImportData("select * from Activists");
                activist = (Activist)hash[IdNumber]; //email
                responseMessage = System.Text.Json.JsonSerializer.Serialize(activist);
                return responseMessage;
            }
            catch (Exception ex)
            {
                MainManager.Instance.logger.Exception($"Activist/GetActivistById : {ex.Message}", ex);
                return ex;
            }
        }
    }
}