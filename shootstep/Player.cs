using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shootstep
{
    class Player : IBaseGameObj
    {
        private Point _position;
        private Bitmap _sprite;

        public void SetPosition(Point pos)
        {
            _position = pos;
        }

        public void SetSprite(string dir)
        {
            _sprite = new Bitmap(dir, false);
        }

        public Bitmap GetSprite()
        {
            return _sprite != null ? _sprite 
                : throw new Exception("Sprite not assigned.");
        }

        public Point GetPosition()
        {
            return _position != null ? _position 
                : throw  new Exception("Object coordinates not assigned.");
        }

        public int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
