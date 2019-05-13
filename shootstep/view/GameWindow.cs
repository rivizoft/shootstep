using System.Drawing;

namespace shootstep.view
{
    public class GameWindow : System.Windows.Forms.Form
    {
        private Game _game;
        
        public GameWindow(Game game)
        {
            _game = game;
            this.MaximumSize = new Size(512,512);
            this.MinimumSize = this.MaximumSize;
            this.Size = this.MaximumSize;
            
        }
        
    }
}