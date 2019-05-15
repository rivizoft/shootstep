using System;
using System.Drawing;

namespace shootstep
{
    public class DubstepHolyRay : IBaseGameObj
    {
        public Point SpeedVector { get; set; }
        public Bitmap Sprite { get; set; }
        public Point Position { get; set; }
        public Rectangle Bbox { get; set; }
        public Bitmap SpriteGlow { get; set; }

        public DubstepHolyRay(Point point, Bitmap sprite, Rectangle bbox, Bitmap spriteGlow)
        {
            Sprite = sprite;
            Position = point;
            Bbox = bbox;
            SpriteGlow = spriteGlow;
        }

        public void MoveTo(Point vector)
        {
            var position = Position;
            position.X = vector.X;
            position.Y = vector.Y;
            Position = position;
        }

        public void InvokeCollision(IBaseGameObj other) => Collision?.Invoke(other);
        public event Action<IBaseGameObj> Collision;
        public event Action Moved;
    }
}