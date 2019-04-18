/* Authors: Blake, Clayton, Dylan
 * File Name: IPlayer.cs
 * 
 * Description: Interface that outlines common functionality between players
 * 
 * 
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardLibrary;

namespace DurakXtreme
{
    public interface IPlayer
    {
        /// <summary>
        /// Every player must have a hand of cards
        /// </summary>
        List<PlayingCard> Cards { get; set; }

        /// <summary>
        /// Name of player
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Status of players turn
        /// </summary>
        TurnStatus TurnStatus { get; set; }

        /// <summary>
        /// Is the player in a condition where they can put down cards. 
        /// (End of a sucessful attack)
        /// </summary>
        bool PuttingDown { get; set; }
    }
}
