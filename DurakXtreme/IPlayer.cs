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
    }
}
