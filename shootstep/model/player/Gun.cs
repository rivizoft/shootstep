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
        public Point Position { get; set; }
        public Rectangle Bbox { get; set; }

        public Gun(Player bindPlayer, Bitmap sprite, Rectangle bbox)
        {
            Position = bindPlayer.Position;
            Sprite = sprite;
            Bbox = bbox;
            bindPlayer.MovePlayer += () => Position = bindPlayer.Position;
        }

        public void MoveTo(Point vector)
        {
            Position = vector;
        }

        public event Action<IBaseGameObj> Collision;
        public event Action MoveGun;
    }
}
