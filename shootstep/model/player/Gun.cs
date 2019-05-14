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
        public Bitmap Sprite { get; set; }
        private float _angle;
        public float Angle
        {
            get => _angle;
            set
            {
                _angle = value;
                Moved?.Invoke();
            }
        }
        public Point Position { get; set; }
        public Rectangle Bbox { get; set; }

        public Gun(IBaseGameObj bindPlayer, Bitmap sprite, Rectangle bbox)
        {
            Position = bindPlayer.Position;
            Sprite = sprite;
            Bbox = bbox;
            Angle = 0;
            bindPlayer.Moved += () => Position = bindPlayer.Position;
        }

        public void MoveTo(Point vector)
        {
            Position = vector;
        }

        public event Action<IBaseGameObj> Collision;
        public event Action Moved;
    }
}
