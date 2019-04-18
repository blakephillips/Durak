/* Authors: Blake, Clayton, Dylan
 * File Name: GameStatistics.cs
 * 
 * Description: Serializable class that holds the statistics and controls writing it to the hard drive.
 * 
 * 
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace DurakXtreme
{
    [Serializable]
    public class GameStatistics
    {

        public int gamesWon { get; set; }
        public int gamesLost { get; set; }
        public int cardsDrawn { get; set; }
        public int attacksWon { get; set; }
        public int defensesRepelled { get; set; }

        private const string FILE_NAME = "stats.dstats";

        public void InitializeStatistics()
        {

            if (!File.Exists(FILE_NAME))
            {
                gamesWon = 0;
                gamesLost = 0;
                cardsDrawn = 0;
                attacksWon = 0;
                defensesRepelled = 0;
                SerializeFile();
            }
            DeserializeFile();
        }

        


        public void SerializeFile()
        {
            GameStatistics gameStats = new GameStatistics();
            IFormatter formatter = new BinaryFormatter();
            Stream fs = new FileStream(FILE_NAME, FileMode.Create, FileAccess.Write);
            formatter.Serialize(fs, this);
            fs.Close();
        }

        private void DeserializeFile()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream fs = new FileStream(FILE_NAME, FileMode.Open, FileAccess.Read);
            GameStatistics gameStatistics = (GameStatistics)formatter.Deserialize(fs);
            fs.Close();

            this.attacksWon = gameStatistics.attacksWon;
            this.cardsDrawn = gameStatistics.cardsDrawn;
            this.defensesRepelled = gameStatistics.defensesRepelled;
            this.gamesLost = gameStatistics.gamesLost;
            this.gamesWon = gameStatistics.gamesWon;
        }


        public void ResetStatistics()
        {
            File.Delete(FILE_NAME);
            InitializeStatistics();
        }


    }
}
