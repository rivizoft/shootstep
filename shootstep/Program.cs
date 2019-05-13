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
            var mainWin = new GameWindow(new Game(), new Size(512,512));
            Application.Run(mainWin);
        }
    }
}
