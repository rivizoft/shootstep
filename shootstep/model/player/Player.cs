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
        private Rectangle _bbox;

        public Player(Point position, Bitmap sprite, Rectangle bbox)
        {
            _position = position;
            _sprite = sprite;
            _bbox = bbox;
        }
        
        public Player(int x, int y, Bitmap sprite, int bboxX,int bboxY, int bboxWidth, int bboxHeigth) 
            : this (new Point(x,y), sprite, new Rectangle(bboxX, bboxY, bboxWidth, bboxHeigth)) {}

        public void MoveTo(Point vector)
        {
            _position.X += vector.X;
            _position.Y += vector.Y;
        }

        public void ForceAbsolutePosition(Point pos)
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

        public override int GetHashCode()
        {
            return _position.GetHashCode();
        }

        public Rectangle GetBbox()
        {
            return _bbox;
        }

        public void ForceBbox(Rectangle bbox)
        {
            _bbox = bbox;
        }

        public event Action<IBaseGameObj> Collision;
    }
}
