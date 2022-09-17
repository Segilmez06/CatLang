using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatLang.LogSystem
{
    /// <summary>
    /// Simple log manager
    /// </summary>
    public class Logger
    {
        string LogFile = "CatLang_latest.log";
        string CreationDate = "";

        string NewLine = Environment.NewLine;
        
        /// <summary>
        /// Initializes log manager
        /// </summary>
        public Logger()
        {
            CreationDate = $"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}_{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}";
            if (File.Exists(LogFile))
            {
                File.Delete(LogFile);
            }
            File.CreateText(LogFile);
            Thread.Sleep(1000);
        }

        /// <summary>
        /// Appends line to log file
        /// </summary>
        /// <param name="Line"></param>
        public void Write(string Line)
        {
            File.AppendAllText(LogFile, string.Concat(Line, NewLine)); // IO Exception pops up
            Thread.Sleep(75);
        }
        
        /// <summary>
        /// Closes the log manager
        /// </summary>
        public void Close()
        {
            string log = string.Concat(CreationDate, ".log");
            if (File.Exists(log))
            {
                File.Delete(log);
                Thread.Sleep(25);
            }
            File.Copy(LogFile, log);
        }
    }
}
