using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer
{
    /// <summary>
    /// This static class Log contains a variety of methods to be called by any other class in order to log text to log files
    /// </summary>
    public static class Log
    {
        private static string outLocation = Assembly.GetExecutingAssembly().Location + "/../../../../Content/logs"; // The location of the folder containing logs

        /// <summary>
        /// Creates a empty log file
        /// </summary>
        private static void createOutput()
        {
            File.WriteAllText(outLocation + "/log.txt", String.Empty);
        }

        /// <summary>
        /// Adds text to a log file
        /// </summary>
        /// <param name="msg"></param> The text to be added
        private static void appendOutput(string msg)
        {
            using (StreamWriter sw = File.AppendText(outLocation + "/log.txt"))
            {
                sw.WriteLine(msg);
            }
        }

        /// <summary>
        /// A standard method to be called by other classes which adds text to a log file
        /// </summary>
        /// <param name="msg"></param>
        public static void logText(string msg)
        {
            if (!File.Exists(outLocation + "/log.txt"))
            {
                createOutput();
            }
            appendOutput(msg);
        }
    }
}
