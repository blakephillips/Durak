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
        List<PlayingCard> Cards { get; set; }
        string Name { get; set; }
        TurnStatus TurnStatus { get; set; }

        /// <summary>
        /// Is the player in a condition where they can put down cards. 
        /// (End of an attack)
        /// </summary>
        bool PuttingDown { get; set; }
    }
}
