using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace shootstep
{
    public class Map : HashSet<IBaseGameObj>
    //TODO: make it proper way
    {
        //private HashSet<IBaseGameObj> map;
        private int _width;
        private int _heigth;

        public Map(int width, int heigth, params IBaseGameObj[] content)
        {
            _width = width;
            _heigth = heigth;

            foreach (var c in content)
            {
                this.Add(c);
            }
        }

        public void AddObject(IBaseGameObj gameObj, bool collisionReplace)
        {
            this.Add(gameObj);
        }
        
        public bool PositionFree(int x, int y) => this.Count(i => i.GetHashCode() == 
                                                                 new Point(x,y).GetHashCode()) == 0;
    }
}