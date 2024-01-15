using System;
using System.IO;
using System.Reflection;

namespace Commangineer
{
    /// <summary>
    /// This static class Log contains a variety of methods to be called by any other class in order to log text to log files
    /// </summary>
    public static class Log
    {
        private static string outLocation = Assembly.GetExecutingAssembly().Location + "/../Content/logs"; // The location of the folder containing logs
        private static string logName = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "-" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + ".txt";

        /// <summary>
        /// Creates a empty log file
        /// </summary>
        private static void CreateOutput()
        {
            if (Directory.Exists(outLocation))
            {
                File.WriteAllText(outLocation + "/" + logName, String.Empty);
            }
            else
            {
                Directory.CreateDirectory(outLocation);
                CreateOutput();
            }
        }

        /// <summary>
        /// Adds text to a log file
        /// </summary>
        /// <param name="msg">The text to be added</param>
        private static void AppendOutput(string msg)
        {
            using (StreamWriter sw = File.AppendText(outLocation + "/" + logName))
            {
                sw.WriteLine(msg);
            }
        }

        /// <summary>
        /// A standard method to be called by other classes which adds text to a log file
        /// </summary>
        /// <param name="msg">The text to be added</param>
        public static void LogText(string msg)
        {
            if (!File.Exists(outLocation + "/" + logName))
            {
                CreateOutput();
            }
            AppendOutput(msg);
        }
    }
}