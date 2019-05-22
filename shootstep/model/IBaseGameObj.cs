using System;
using System.Drawing;

namespace shootstep
{
    public interface IBaseGameObj
    {
        Point SpeedVector { get; set; }
        int Health { get; set; }
        Bitmap Sprite { get; set; }
        Point Position { get; set; }
        Rectangle Bbox { get; set; }
        Bitmap SpriteGlow { get; set; }
        void MoveTo(Point vector);
        void Move();
        int GetHashCode();
        event Action<IBaseGameObj> Collision;
        void InvokeCollision(IBaseGameObj other);
        event Action Moved;
    }
}