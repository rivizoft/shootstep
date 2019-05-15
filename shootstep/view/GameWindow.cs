using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace shootstep.view
{
    public sealed class GameWindow : Form
    {
        private readonly Game _game;
        private Size _defaultSize;
        private Camera _camera;
        
        public GameWindow(Game game, Size size)
        {
            _game = game;
            _defaultSize = new Size(size.Width, size.Height);
            this.Init(size);
            this.SetControls(game);
            this.DoubleBuffered = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Invalidate();

            // �� ��������
            SetStyle(ControlStyles.OptimizedDoubleBuffer | 
                     ControlStyles.AllPaintingInWmPaint | 
                     ControlStyles.UserPaint, true);
        }

        private void Init(Size size)
        {
            _game.Update += this.Invalidate;
            this.MaximumSize = size;
            this.MinimumSize = this.MaximumSize;
            this.Size = this.MaximumSize;
            this._camera = new Camera(_game.GetPlayer(), this.Width, this.Height);
            this._camera.Moved += this.Invalidate;
            this.Controls.Clear();
        }

        private void SetControls(Game game)
        {
            KeyDown += (sender, args) =>
            {
                var s = game.GetPlayer().SpeedVector;
                if (args.KeyCode == Keys.W)
                    s.Y = -8;
                if (args.KeyCode == Keys.S)
                    s.Y = 8;
                if (args.KeyCode == Keys.A)
                    s.X = -8;
                if (args.KeyCode == Keys.D)
                    s.X = 8;
                game.GetPlayer().SpeedVector = s;
                game.CursorPosition = new Point(MousePosition.X - _camera.GetViewPoint().X,
                    MousePosition.Y - _camera.GetViewPoint().Y);
            };

            MouseMove += (sender, args) => game.CursorPosition = new Point(args.Location.X - _camera.GetViewPoint().X,
                args.Location.Y - _camera.GetViewPoint().Y);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var shift = _camera.GetViewPoint();
            var graphics = e.Graphics;

            graphics.Clear(Color.FromArgb(54, 54, 54));

            foreach (var o in _game.GetMap())
                if (o.GetType() != typeof(Gun))
                {
                    graphics.DrawImage(o.SpriteGlow, new Point(o.Position.X + shift.X - o.Sprite.Width * 3 / 4 - 2,
                        o.Position.Y + shift.Y - o.Sprite.Height * 3 / 4 - 2));
                    graphics.DrawImage(o.Sprite, new Point(o.Position.X + shift.X - o.Sprite.Width / 2,
                        o.Position.Y + shift.Y - o.Sprite.Height / 2));
                }
                else
                {
                    graphics.TranslateTransform(o.Position.X + shift.X + o.Sprite.Width / 2,
                        o.Position.Y + shift.Y + o.Sprite.Height / 2);
                    graphics.RotateTransform((((Gun)o).Angle + (float)(Math.Atan(Math.PI / 2) * 90)) % 360);
                    graphics.DrawImage(o.Sprite, new Point(-o.Sprite.Width / 2, -2 * o.Sprite.Height));
                    graphics.ResetTransform();
                }

            

            _camera.Update();
        }
    }
}