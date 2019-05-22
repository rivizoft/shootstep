using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Media;
using System.Runtime.Remoting.Channels;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using NAudio.Wave;
using shootstep.model.config;

namespace shootstep.view
{
    public sealed class GameWindow : Form
    {
        private readonly Game _game;
        private Size _defaultSize;
        private Camera _camera;
        private SoundContainer _sound;

        public GameWindow(Size size)
        {
            _game = new Game();
            _defaultSize = new Size(size.Width, size.Height);
            this.Init(size);
            this.SetControls(_game);
            _game.Update += MoveDust;
            this.DoubleBuffered = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Invalidate();

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
            _game.GetGlobalOptions().WindowSize = this.Size;
            this._camera = new Camera(_game.GetPlayer(), this.Width, this.Height);
            //this._camera.Moved += this.Invalidate;
            _game.Update += _camera.Update;
            Globals.Init(2048,2048, _camera, _game.GetPlayer());
            _game.GetMap().SetSize(Globals.GetGlobalInfo().MapOptions.Width,
                Globals.GetGlobalInfo().MapOptions.Height);
            _game.AddDust();
            this.Controls.Clear();
        }

        private void MoveDust()
        {
            var speedX = 1;
            var speedY = 1;
            var random = new Random();
            var dust = _game.GetDust();

            if (_sound.CurrentSoundPosition != 0)
            {
                speedX *= 20;
                speedY *= 20;
            }
            else
            {
                speedX = 1;
                speedY = 1;
            }

            foreach (var d in dust)
            {
                var position = d.Position;
                position.X += random.Next(-speedX, speedX + 1);
                position.Y += random.Next(-speedY, speedY + 1);
                d.Position = position;
            }
        }

        private void SetControls(Game game)
        {
            _sound = new SoundContainer();
            _sound.Init(resourses.sound1, resourses.sound2, 
                resourses.sound3, resourses.sound4);

            MouseDown += (sender, args) =>
            {
                _sound.PlayLooping();
            };

            MouseUp += (sender, args) =>
            {
                _sound.Stop();
            };

            KeyDown += (sender, args) =>
            {
                var speed = 24;
                var s = game.GetPlayer().SpeedVector;
                if (args.KeyCode == Keys.W)
                    s.Y = -speed;
                if (args.KeyCode == Keys.S)
                    s.Y = speed;
                if (args.KeyCode == Keys.A)
                    s.X = -speed;
                if (args.KeyCode == Keys.D)
                    s.X = speed;
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
                        graphics.DrawImage(o.Sprite, new Point(o.Position.X + shift.X - o.Sprite.Width / 2,
                            o.Position.Y + shift.Y - o.Sprite.Height / 2));
                }
                else
                {
                    graphics.TranslateTransform(
                        _game.GetPlayer().Position.X + shift.X, //+ _game.GetPlayer().Sprite.Width / 4,
                        _game.GetPlayer().Position.Y + shift.Y);// + _game.GetPlayer().Sprite.Height / 4);
                    graphics.RotateTransform((((Gun)o).Angle + (float)(Math.Atan(Math.PI / 2) * 90)) % 360);
                    graphics.DrawImage(o.Sprite, new Point(-o.Sprite.Width / 2, -2 * o.Sprite.Height));
                    graphics.ResetTransform();
                }

            graphics.DrawString(Globals.GetGlobalInfo().Player.Health.ToString(), new Font("Arial", 12), 
                new SolidBrush(Color.Beige), 32, 32);
            
            _camera.Update();
        }
    }
}