using System;
using System.Drawing;

namespace shootstep
{
    public interface IBaseGameObj
    {
        Bitmap Sprite { get; set; }
        Point Position { get; set; }
        Rectangle Bbox { get; set; }
        void MoveTo(Point vector);
        int GetHashCode();
        event Action<IBaseGameObj> Collision;
    }
}