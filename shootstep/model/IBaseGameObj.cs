using System;
using System.Drawing;

namespace shootstep
{
    public interface IBaseGameObj
    {
        //TODO: добавить параметры скоростей по горизонтали и вертикали и логику движения "пока скорость != 0"
        //это нужно для работы коллизий (по коллизии скорость ставится 0 и всё встаёт ;) )
        Point SpeedVector { get; set; }
        Bitmap Sprite { get; set; }
        Point Position { get; set; }
        Rectangle Bbox { get; set; }
        Bitmap SpriteGlow { get; set; }
        void MoveTo(Point vector);
        int GetHashCode();
        event Action<IBaseGameObj> Collision;
        void InvokeCollision(IBaseGameObj other);
        event Action Moved;
    }
}