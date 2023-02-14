using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoIt.Entitis
{
    public class Logger
    {
        public Logger(providerType provider)
        {
            Run(provider);
            MyLog.Init();
        }
        public static ILog MyLog { get; set; }

        public enum providerType
        {
            logFile, logConsole, logDB
        }

        public ILog Run(providerType aProvider)
        {
            if (aProvider == providerType.logFile)
            {
                MyLog = new LogFile();
            }
            else if (aProvider == providerType.logDB)
            {
                MyLog = new LogDB();
            }
            else if (aProvider == providerType.logConsole)
            {
                MyLog = new LogConsole();
            }

            return MyLog;
        }

        public void Event(string msg)
        {
            MyLog.LogEvent(msg);
        }
        public void Error(string msg)
        {
            MyLog.LogError(msg);
        }
        public void Exception(string msg, Exception exce)
        {
            MyLog.LogException(msg, exce);
        }
    }

    public interface ILog
    {
        void Init();
        void LogEvent(string Message);
        void LogError(string Message);
        void LogException(string Message, Exception exce);
        void LogCheckHoseKeeping();
    }

}

