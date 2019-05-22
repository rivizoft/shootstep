using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace shootstep
{
    public class Dust : IBaseGameObj
    {
        public Point SpeedVector { get; set; }
        public int Health { get; set; }
        public Bitmap Sprite { get; set; }
        public Point Position { get; set; }
        public Rectangle Bbox { get; set; }
        public Bitmap SpriteGlow { get; set; }

        public Dust(Point position, Bitmap sprite, Rectangle bbox)
        {
            Position = position;
            Sprite = sprite;
            Health = 2;
            Bbox = bbox;
        }

        public void MoveTo(Point vector)
        {
            var position = Position;
            position.X = vector.X;
            position.Y = vector.Y;
            Position = position;
        }

        public void Move()
        {
            
        }

        public void InvokeCollision(IBaseGameObj other)
        {
            throw new NotImplementedException();
        }

        public event Action Moved;
        public event Action<IBaseGameObj> Collision;
    }
}
