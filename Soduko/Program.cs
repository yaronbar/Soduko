using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Soduko
{

    using System;
    using System.Collections.Generic;
    using System.Text;
    using Interface;

    public class Program
    {
        public static void Main()
        {
            //// Create a new FormGame and activate it:
            FormGame SodukoGame = new FormGame();
            SodukoGame.ShowDialog();
        }
    }
}
