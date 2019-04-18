/* Authors: Blake, Clayton, Dylan
 * File Name: GameplayLog.cs
 * 
 * Description: Controls displaying log data to console and writing it to a log file
 * 
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
           
            Console.WriteLine(logData);

            try
            {
                DateTime now = DateTime.Now;
                StreamWriter sw = new StreamWriter("log.txt", true);
                sw.WriteLine(now.ToShortDateString() +": "+ logData);
                sw.Close();
            } catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }


        }
    }
}
