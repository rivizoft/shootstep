using System.Drawing;
using System.Windows.Forms;

namespace shootstep.view
{
    public class GameWindow : System.Windows.Forms.Form
    {
        private Game _game;
        private PictureBox _canvas;
        private Size _defaultSize;
        
        public GameWindow(Game game, Size size)
        {
            _game = game;
            _defaultSize = new Size(size.Width, size.Height);
            this.Init(size);
            this.Invalidate();
        }

        private void Init(Size size)
        {
            this.MaximumSize = size;
            this.MinimumSize = this.MaximumSize;
            this.Size = this.MaximumSize;
            
            _canvas = new PictureBox();
            _canvas.BackColor = Color.Black;
            _canvas.Size = this.Size;
            //this.Resize += (sender, args) => _canvas.Scale();
            this.Controls.Clear();
            //this.Controls.Add(_canvas);
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