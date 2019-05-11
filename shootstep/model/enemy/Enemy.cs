using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shootstep
{
    class Enemy : IBaseGameObj
    {
        public void SetSprite(string dir)
        {
            throw new NotImplementedException();
        }

        public Bitmap GetSprite()
        {
            throw new NotImplementedException();
        }

        public void MoveTo(Point vector)
        {
            throw new NotImplementedException();
        }

        public void ForceAbsolutePosition(Point pos)
        {
            throw new NotImplementedException();
        }

        public Point GetPosition()
        {
            throw new NotImplementedException();
        }

        public Rectangle GetBbox()
        {
            throw new NotImplementedException();
        }

        public void ForceBbox(Rectangle bbox)
        {
            throw new NotImplementedException();
        }

        public event Action<IBaseGameObj> Collision;
    }
}
