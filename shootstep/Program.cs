using System;
using System.Drawing;
using System.Windows.Forms;
using shootstep.view;

namespace shootstep
{
    class Program
    {
        static void Main(string[] args)
        {
            var mainWin = new GameWindow(new Game(), new Size(
                Screen.PrimaryScreen.Bounds.Width * 3 / 4, Screen.PrimaryScreen.Bounds.Height * 3 / 4));
            Application.Run(mainWin);
        }
    }
}
