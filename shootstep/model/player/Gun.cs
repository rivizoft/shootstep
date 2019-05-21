using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shootstep
{
    public class Gun : IBaseGameObj
    {
        public Point SpeedVector { get; set; }
        public Bitmap Sprite { get; set; }
        private float _angle;
        public Point Position { get; set; }
        public Rectangle Bbox { get; set; }
        public Bitmap SpriteGlow { get; set; }

        public float Angle
        {
            get => _angle;
            set
            {
                _angle = value;
                Moved?.Invoke();
            }
        }

        public Gun(IBaseGameObj bindPlayer, Bitmap sprite, Rectangle bbox, Bitmap spriteGlow)
        {
            Position = bindPlayer.Position;
            Sprite = sprite;
            Bbox = bbox;
            Angle = 0;
            bindPlayer.Moved += () => Position = bindPlayer.Position;
            SpriteGlow = spriteGlow;
        }

        public void MoveTo(Point vector)
        {
            Position = vector;
        }

        public void Move()
        {
            
        }

        public void InvokeCollision(IBaseGameObj other) => Collision?.Invoke(other);
        public event Action<IBaseGameObj> Collision;
        public event Action Moved;
    }
}
