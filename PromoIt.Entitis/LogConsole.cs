using System;

namespace PromoIt.Entitis
{
    public class LogConsole : ILog
    {
        public void Init()
        {

        }

        public void LogEvent(string Message)
        {
            Console.WriteLine(Message);
        }

        public void LogError(string Message)
        {
            Console.WriteLine(Message);
        }
        public void LogException(string Message, Exception exce)
        {
            Console.WriteLine($"Error: {Message} Exception: {exce.Source}");
        }
        public void LogCheckHoseKeeping()
        {

        }
    }
}