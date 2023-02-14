using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PromoIt.Entitis
{
    public class LogDB : ILog
    {
        public string TypeLog { get; set; }
        public string LogMessage { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;


        public string DeleteQuery { get; set; } = "delete from LOGS where Date < DATEADD(month, -3, GETDATE())";

        Queue<string> LogQueue;
        Task queueTask = null;
        bool stop = false;

        public void Init()
        {
            LogQueue = new Queue<string>();
            popLogFromQueue();
            LogCheckHoseKeeping();
        }

        public void LogEvent(string Message)
        {
            TypeLog = "Event";
            LogMessage = Message;
            LogQueue.Enqueue($"insert into LOGS values('{TypeLog}','{LogMessage}','{Date}')");
        }

        public void LogError(string Message)
        {
            TypeLog = "Error";
            LogMessage = Message;
            LogQueue.Enqueue($"insert into LOGS values('{TypeLog}','{LogMessage}','{Date}')");
        }

        public void LogException(string Message, Exception exce)
        {
            TypeLog = "Exception";
            LogMessage = $"{DateTime.Now} Error: {Message} Exception: {exce.Source}";
            LogQueue.Enqueue($"insert into LOGS values('{TypeLog}','{LogMessage}','{Date}')");
        }

        public void LogCheckHoseKeeping()
        {
            Task.Run(() =>
            {
                while (!stop)
                {
                    // Delete from DB
                    ExportFromDB(DeleteQuery);
                    // check the DB takes 1 day
                    System.Threading.Thread.Sleep(1000 * 60 * 60 * 24);
                }
            });

        }

        private void popLogFromQueue() // pop
        {
            queueTask = Task.Run(() =>
            {
                while (!stop)
                {
                    if (LogQueue.Count > 0)
                    {
                        string InsertQuery = LogQueue.Dequeue();
                        ExportFromDB(InsertQuery);

                        // save item to DB takes 1 second
                        System.Threading.Thread.Sleep(1000);
                    }

                    System.Threading.Thread.Sleep(1000);
                }

            });
        }

        // ייצוא נתונים - 1
        // Gives a command to DAL to create a connection with SQL for Export
        public void ExportFromDB(string SqlQuery)
        {
            DAL.PromoItQuery.InputToDB(SqlQuery, changeTheDB);
        }

        // ייצוא נתונים - 2
        // Exports the data from the server into the database
        public void changeTheDB(SqlCommand command)
        {
            command.ExecuteNonQuery();
        }
    }
}