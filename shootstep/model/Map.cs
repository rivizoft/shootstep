using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace shootstep
{
    public class Map
    //TODO: make it proper way
    {
        private HashSet<IBaseGameObj> map;
        private int width;
        private int heigth;

        public Map(int width, int heigth, params IBaseGameObj[] content)
        {
            map = new HashSet<IBaseGameObj>();
            this.width = width;
            this.heigth = heigth;
            foreach (var c in content)
            {
                map.Add(c);
            }
        }

        public void AddObject(IBaseGameObj gameObj, bool collisionReplace)
        {
            map.Add(gameObj);
        }
        
        public bool PositionFree(int x, int y) => map.Count(i => i.GetHashCode() == 
                                                                 new Point(x,y).GetHashCode()) == 0;
    }
}