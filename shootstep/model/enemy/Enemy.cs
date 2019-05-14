using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shootstep
{
    class Enemy : IBaseGameObj
    {
        public Bitmap Sprite { get; set; }
        public Point Position { get; set; }
        public Rectangle Bbox { get; set; }

        public Enemy(Point position, Bitmap sprite, Rectangle bbox)
        {
            Position = position;
            Sprite = sprite;
            Bbox = bbox;
        }

        public void MoveTo(Point vector)
        {
            var position = Position;
            position.X = vector.X;
            position.Y = vector.Y;
            Position = position;
        }

        public event Action<IBaseGameObj> Collision;
        public event Action Moved;
        
    }
}
