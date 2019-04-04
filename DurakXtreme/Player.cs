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
        /// Depending on turnStatus, set off appopriate EventHandler.
        /// </summary>
        private TurnStatus myTurnStatus;
        public TurnStatus CurrentTurnStatus
        {
            get { return myTurnStatus; }
            set
            {
                if (value == TurnStatus.Attacking)
                {
                    Player_Attacking();
                } else if (value == TurnStatus.Defending)
                {
                    Player_Defending();
                }
                myTurnStatus = value;
            }
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

        new public EventHandler Defending;
        new public EventHandler Attacking;
        /// <summary>
        /// If Defending Event Handler is set, 
        ///     set off EventHandler.
        /// </summary>
        private void Player_Defending()
        {
            if (Defending != null)
                Defending(this, new EventArgs());
        }

        /// <summary>
        /// If Attacking Event Handler is set,
        ///     set off EventHandler.
        /// </summary>
        private void Player_Attacking()
        {
            if (Attacking != null)
                Attacking(this, new EventArgs());
        }


        public override string ToString()
        {
            return Name;
        }

    }
}
