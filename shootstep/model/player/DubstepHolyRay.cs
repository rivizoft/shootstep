using System;
using System.Drawing;

namespace shootstep
{
    public class DubstepHolyRay : IBaseGameObj
    {
        public Point SpeedVector { get; set; }
        public int Health { get; set; }
        public Bitmap Sprite { get; set; }
        public Point Position { get; set; }
        public Rectangle Bbox { get; set; }
        public Bitmap SpriteGlow { get; set; }

        public DubstepHolyRay(Point point, Bitmap sprite, Rectangle bbox, Bitmap spriteGlow, Point impulse)
        {
            Sprite = sprite;
            Position = point;
            Bbox = bbox;
            Health = 1;
            SpriteGlow = spriteGlow;
            SpeedVector = impulse;
            InitCollisions();
        }

        private void InitCollisions()
        {
            Collision += (other) =>
            {
                switch (other)
                {
                    case Enemy e:
                        Health--;
                        break;
                }
            };
        }
        
        public void MoveTo(Point vector)
        {
            var position = Position;
            position.X = vector.X;
            position.Y = vector.Y;
            Position = position;
            Moved.Invoke();
        }

        public void Move()
        {
            MoveTo(SpeedVector);
        }

        public void InvokeCollision(IBaseGameObj other) => Collision?.Invoke(other);
        public event Action<IBaseGameObj> Collision;
        public event Action Moved;
    }
}