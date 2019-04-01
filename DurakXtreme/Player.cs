using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardLibrary;

namespace DurakXtreme
{
    public class Player : List<PlayingCard>
    {
        /// <summary>
        /// Constructor that includes name of player
        /// </summary>
        /// <param name="pName"></param>
        public Player(String pName)
        {
            Name = pName;
        }

        /// <summary>
        /// If the player is attacking or defending currently.
        /// </summary>
        private TurnStatus myTurnStatus;
        public TurnStatus CurrentTurnStatus
        {
            get { return myTurnStatus; }
            set { myTurnStatus = value; }
        }
        /// <summary>
        /// Name of the player to be displayed
        /// </summary>
        private string myPlayerName;
        public string Name
        {
            get { return myPlayerName; }
            set { myPlayerName = value; }
        }

        public override string ToString()
        {
            return Name;
        }

    }
}
