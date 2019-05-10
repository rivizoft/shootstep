using System.Drawing;

namespace shootstep
{
    public interface BaseGameObj
    {
        Bitmap GetSprite();
        Point GetPosition();
        int GetHashCode();
    }
}