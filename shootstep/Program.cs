using System;
using System.Windows.Forms;
using shootstep.view;

namespace shootstep
{
    class Program
    {
        static void Main(string[] args)
        {
            var mainWin = new GameWindow(new Game());
            Application.Run(mainWin);
        }
    }
}
