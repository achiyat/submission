using PromoIt.Entities;
using PromoIt.Model;
using System.Collections;
using System;

namespace PromoIt.Entitis.Commnd
{
    public class message : ICommand
    {
        public object ExecuteCommand(params object[] param)
        {
            ICommand command = MainManager.Instance.commandManager.CommandList["message"];
            Message data = new Message();
            string Query;

            string requestBody = (string)param[0];

            try
            {
                MainManager.Instance.logger.Event($"User/message : insert new message");
                data = System.Text.Json.JsonSerializer.Deserialize<Message>(requestBody);
                Query = "insert into messages values(@ID,@Name,@Phone,@Email,@Message)";
                MainManager.Instance.Messages.ExportFromDB(Query, data);
                return null;
            }
            catch (Exception ex)
            {
                MainManager.Instance.logger.Exception($"User/message : {ex.Message}", ex);
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
            Shipping shipping = new Shipping();

            string Query;

            string requestBody = (string)param[0];
            string IdNumber = (string)param[1];
            string IdNumber1 = (string)param[2];
            string responseMessage;

            try
            {
                return responseMessage;
            }
            catch (Exception ex)
            {
                MainManager.Instance.logger.Event($"1111111111111111111");
                MainManager.Instance.logger.Exception($"1111111111111111111 : {ex.Message}", ex);
                return ex;
            }
        }
 */