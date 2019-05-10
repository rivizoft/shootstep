using System.Drawing;

namespace shootstep
{
    public interface IBaseGameObj
    {
        Bitmap GetSprite();
        Point GetPosition();
        int GetHashCode();
    }
}