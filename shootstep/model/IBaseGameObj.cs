using System;
using System.Drawing;

namespace shootstep
{
    public interface IBaseGameObj
    {
        void SetSprite(string dir);
        Bitmap GetSprite();
        void MoveTo(Point vector);
        void ForceAbsolutePosition(Point pos);
        Point GetPosition();
        int GetHashCode();
        Rectangle GetBbox();
        void ForceBbox(Rectangle bbox);
        event Action<IBaseGameObj> Collision;
    }
}