using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms.VisualStyles;

namespace shootstep
{
    public class Game
    {
        //TODO: implement Game as main model controller
        private Map _map;
        private Player _player;
        private Gun _gun;
        private Point _cursorPosition;
        public Point CursorPosition
        {
            get => _cursorPosition;
            set
            {
                _cursorPosition = value; 
                CursorUpdate?.Invoke(value);
            }
        }
        
        public Game(int mapWidth, int mapHeight)
        {
            _map = new Map(mapWidth, mapHeight);
            _player = new Player(new Point(0,0), resourses.Player, new Rectangle(0, 0, 32, 32), resourses.Player);
            _gun = new Gun(_player, resourses.Gun, new Rectangle(0, 0, 0, 0), resourses.Gun);
            this.AddToMap(_player,_gun, 
                new Enemy(new Point(64, 64), resourses.Enemy, new Rectangle(0,0,0,0), resourses.Enemy));

            CursorUpdate += point => _gun.Angle = (float)((Math.Atan2(point.Y - _gun.Position.Y, point.X - _gun.Position.X)
                                                    + 2 * Math.PI) * 180 / Math.PI) % 360;
            this.Update += () => _player.UpdatePosition();
        }

        public Map GetMap()
        {
            return _map;
        }

        public Player GetPlayer()
        {
            return _player;
        }

        public void AddToMap(params IBaseGameObj[] gameObjects)
        {
            foreach (var o in gameObjects)
            {
                o.Moved += () => Update?.Invoke();
                _map.AddObject(o, false);
            }
        } 

        public event Action Update;
        private event Action<Point> CursorUpdate;

    }
}