using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shootstep
{
    public class Player : IBaseGameObj
    {
        public Bitmap Sprite { get; set; }
        public Point Position { get; set; }
        public Rectangle Bbox { get; set; }

        public Player(Point position, Bitmap sprite, Rectangle bbox)
        {
            Position = position;
            Sprite = sprite;
            Bbox = bbox;
        }
        
        public Player(int x, int y, Bitmap sprite, int bboxX, int bboxY, int bboxWidth, int bboxHeigth) 
            : this (new Point(x,y), sprite, new Rectangle(bboxX, bboxY, bboxWidth, bboxHeigth)) {}

        public void MoveTo(Point vector)
        {
            var position = Position;
            position.X += vector.X;
            position.Y += vector.Y;
            Position = position;
            MovePlayer.Invoke();
        }

        public override int GetHashCode()
        {
            return Position.GetHashCode();
        }

        public event Action<IBaseGameObj> Collision;
        public event Action MovePlayer;
    }
}
