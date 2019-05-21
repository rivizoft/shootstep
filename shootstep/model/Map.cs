using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace shootstep
{
    public class Map : HashSet<IBaseGameObj>
    //TODO: добавить обработчик коллизий
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
                AddObject(c, false);
            }
        }

        public void AddObject(IBaseGameObj gameObj, bool collisionReplace)
        {
            var p = gameObj.Position;
            p.X %= _width;
            p.Y %= _heigth;
            gameObj.Position = p;
            gameObj.Moved += () => CheckCollisions(gameObj);
            UpdateMap += gameObj.Move;
            this.Add(gameObj);
            if (gameObj.GetType() == typeof(Enemy)) Globals.GetGlobalInfo().Enemy.Count++;
        }

        public void CheckCollisions(IBaseGameObj gameObj)
        {
            foreach (var other in this)
            {
                if (other != gameObj)
                {
                    var bbox1 = gameObj.Bbox;
                    bbox1.Location = gameObj.Position;
                    var bbox2 = other.Bbox;
                    bbox2.Location = other.Position;
                    if (bbox1.IntersectsWith(bbox2))
                    {
                        Console.WriteLine("Collision");
                        gameObj.InvokeCollision(other);
                    }
                }
            }
        }
        
        public bool PositionFree(int x, int y) => this.Count(i => i.GetHashCode() == 
                                                                 new Point(x,y).GetHashCode()) == 0;

        public void Update()
        {
            UpdateMap.Invoke();
        }

        public void SetSize(int width, int height)
        {
            _width = width;
            _heigth = height;
        }

        private Action UpdateMap;
    }
}