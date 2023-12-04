using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer
{
    public static class Log
    {
        private static string outLocation = Assembly.GetExecutingAssembly().Location + "/../../../../Content/logs";

        private static void createOutput(string msg)
        {
            File.WriteAllText(outLocation + "/log.txt", String.Empty);
        }

        private static void appendOutput(string msg)
        {
            using (StreamWriter sw = File.AppendText(outLocation + "/log.txt"))
            {
                sw.WriteLine(msg);
            }
        }

        public static void logText(string msg)
        {
            if (!File.Exists(outLocation + "/log.txt"))
            {
                createOutput(msg);
            }
            appendOutput(msg);
        }
    }
}
