using PromoIt.Entities;
using PromoIt.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace PromoIt.Entitis.Commnd
{
    public class AddActivist : ICommand
    {
        object ICommand.ExecuteCommand(params object[] param)
        {
            
            ICommand command = MainManager.Instance.commandManager.CommandList["AddActivist"];
            Activist Activist = new Activist();
            string Query;

            string requestBody = (string)param[0];
            string VarActivist = (string)param[1];


            try
            {
                MainManager.Instance.logger.Event("Activist/AddActivist : insert new Activist");
                Activist = System.Text.Json.JsonSerializer.Deserialize<Activist>(requestBody);
                Query = $"insert into Activists values({VarActivist})";
                MainManager.Instance.Activists.ExportFromDB(Query, Activist);
                return null;
            }
            catch (Exception ex)
            {
                MainManager.Instance.logger.Exception($"Activist/AddActivist : {ex.Message}", ex);
                return ex;
            }
        }
    }
}

