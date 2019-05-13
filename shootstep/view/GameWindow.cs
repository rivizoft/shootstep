using System.Drawing;
using System.Windows.Forms;

namespace shootstep.view
{
    public class GameWindow : Form
    {
        private readonly Game _game;
        private Size _defaultSize;
        
        public GameWindow(Game game, Size size)
        {
            _game = game;
            _defaultSize = new Size(size.Width, size.Height);
            this.Init(size);
            this.SetControls(game);
            this.Invalidate();
        }

        private void Init(Size size)
        {
            _game.Update += () => this.Invalidate();
            this.MaximumSize = size;
            this.MinimumSize = this.MaximumSize;
            this.Size = this.MaximumSize;
            this.Controls.Clear();
        }

        private void SetControls(Game game)
        {
            KeyPress += (sender, args) =>
            {
                switch (args.KeyChar)
                {
                    case 'W':
                        game.GetPlayer().MoveTo(new Point(0, -10));
                        break;
                    case 'S':
                        game.GetPlayer().MoveTo(new Point(0, 10));
                        break;
                    case 'A':
                        game.GetPlayer().MoveTo(new Point(-10, 0));
                        break;
                    case 'D':
                        game.GetPlayer().MoveTo(new Point(10, 0));
                        break;
                }
            };
            
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var graphics = e.Graphics;
            graphics.Clear(Color.Black);
            foreach (var o in _game.GetMap())
                graphics.DrawImage(o.Sprite, o.Position);
        }
        
    }
}