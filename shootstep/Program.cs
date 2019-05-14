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
            var mainWin = new GameWindow(new Game(512,512), new Size(
                Screen.PrimaryScreen.Bounds.Width * 7 / 8, Screen.PrimaryScreen.Bounds.Height * 7 / 8));
            Application.Run(mainWin);
        }
    }
}
