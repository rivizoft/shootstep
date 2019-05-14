using System;
using System.Drawing;

namespace shootstep
{
    public class DubstepHolyRay : IBaseGameObj
    {
        public Bitmap Sprite { get; set; }
        public Point Position { get; set; }
        public Rectangle Bbox { get; set; }

        public DubstepHolyRay(Point point, Bitmap sprite, Rectangle bbox)
        {
            Sprite = sprite;
            Position = point;
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