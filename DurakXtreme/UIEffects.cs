using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace DurakXtreme
{
    public class UIEffects
    {
        /// <summary>
        /// Pops a button. 
        /// </summary>
        /// <param name="btn">Button to pop</param>
        /// <param name="isEntering">True if mouse is entering, false otherwise</param>
        /// <param name="popSize">Amount to add to width and height</param>
        public static void btnPop(Button btn, bool isEntering = true, int popSize = 20)
        {
            if (isEntering == true)
            {
                btn.Location = new Point(btn.Location.X - (popSize / 2), btn.Location.Y - (popSize / 2));
                btn.Size = new Size(btn.Width + popSize, btn.Height + popSize);
            }
            else
            {
                btn.Location = new Point(btn.Location.X + (popSize / 2), btn.Location.Y + (popSize / 2));
                btn.Size = new Size(btn.Width - popSize, btn.Height - popSize);
            }
        }
    }
}
