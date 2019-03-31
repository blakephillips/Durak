using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLibrary
{
    public class GameplayLog
    {
        /// <summary>
        /// Logs gameplay actions to both a console and to an external file
        /// </summary>
        /// <param name="logData">String of data to be logged</param>
        public static void Log(string logData)
        {
            //TODO write this data to a file
            Console.WriteLine(logData);
        }
    }
}
