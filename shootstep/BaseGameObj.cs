using System.Drawing;

namespace shootstep
{
    public interface IBaseGameObj
    {
        void SetSprite(string dir);
        Bitmap GetSprite();
        void SetPosition(Point pos);
        Point GetPosition();
        int GetHashCode();
    }
}