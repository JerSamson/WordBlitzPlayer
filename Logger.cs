using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordBlitzPlayer
{

    static class Logger
    {
        static private string BasePath = @"C:\Users\Jeremy\source\repos\WordBlitzPlayer\Logs\";
        static private string LogFileName = DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss");
        static private string LogPath => BasePath + LogFileName + ".txt";

        static private object LogLock = new object();

        public static void NewLog()
        {
            LogFileName = DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss");
        }

        public static void log(string entry)
        {

            try
            {

                // Create a new file
                lock(LogLock)
                using (StreamWriter sw = File.AppendText(LogPath))
                {
                    sw.WriteLine(entry);
                }
            }
            catch
            {
                //Ignore
            }
        }

    }
}
