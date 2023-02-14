using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;

namespace PromoIt.Entitis
{
    public class LogFile : ILog  
    {
        public static string fileOfName { get; set; } = NameOfFile();
        // C:\Users\achiy\source\repos\NewPromoIt\Log
        private readonly string N_file = NameOfFile();

        //System.Collections.Generic.Queue<string> LogQueue;
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
            LogQueue.Enqueue($"{DateTime.Now} Event: {Message} \n");
        }

        public void LogError(string Message)
        {
            LogQueue.Enqueue($"{DateTime.Now} Error: {Message} \n");
        }

        public void LogException(string Message, Exception exce)
        {
            LogQueue.Enqueue($"{DateTime.Now} Error: {Message} Exception: {exce.Source} \n");
        }

        public void LogCheckHoseKeeping()
        {
            Task.Run(() =>
            {
                while (!stop)
                {
                    // check the size of the file
                    var fi1 = new FileInfo(fileOfName + ".log");
                    if (fi1.Length >= 5000000)
                    {
                        //Change File Name
                        ChangeFileName();
                    }
                    // check the size takes 1 Hour
                    System.Threading.Thread.Sleep(1000 * 60 * 60);
                }
            });
        }

        private void ChangeFileName()
        {
            int CountNumberFile = 1;
            var fi1 = new FileInfo(fileOfName + ".log");
            while (System.IO.File.Exists(fileOfName + ".log") && fi1.Length >= 5000000) //1048576
            {
                fileOfName = $@"{N_file}({CountNumberFile})";
                fi1 = new FileInfo(fileOfName + ".log");
                CountNumberFile++;
            }
        }

        private static string NameOfFile()
        {
            string folderName = "NewPromoIt";
            int index = Directory.GetCurrentDirectory().IndexOf(folderName);
            string path = Directory.GetCurrentDirectory().Substring(0, index + folderName.Length);
            string fileName = $"Log\\log {DateTime.Now.ToString("dd-MM-yyyy")}";
            return Path.Combine(path, fileName);
        }

        private void popLogFromQueue() // pop
        {
            queueTask = Task.Run(() =>
            {
                while (!stop)
                {
                    if (LogQueue.Count > 0)
                    {
                        string log = LogQueue.Dequeue();
                        File.AppendAllText(fileOfName + ".log", log);
                        //LogFile Log = LogQueue.Dequeue();

                        // save item to file takes 1 second
                        System.Threading.Thread.Sleep(1000);
                    }

                    System.Threading.Thread.Sleep(1000);
                }

            });
        }
    }

}

