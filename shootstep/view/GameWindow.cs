using System;
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
            KeyDown += (sender, args) =>
            {
                if (args.KeyCode == Keys.W)
                    game.GetPlayer().MoveTo(new Point(0, -8));
                if (args.KeyCode == Keys.S)
                    game.GetPlayer().MoveTo(new Point(0, 8));
                if (args.KeyCode == Keys.A)
                    game.GetPlayer().MoveTo(new Point(-8, 0));
                if (args.KeyCode == Keys.D)
                    game.GetPlayer().MoveTo(new Point(8, 0));
            };
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var shift = new Point(this.Width / 2 - _game.GetPlayer().Position.X, 
                this.Height / 2 - _game.GetPlayer().Position.Y);
            var graphics = e.Graphics;
            graphics.Clear(Color.Black);
            foreach (var o in _game.GetMap())
                graphics.DrawImage(o.Sprite, new Point(o.Position.X + shift.X, o.Position.Y + shift.Y));
        }
        
    }
}