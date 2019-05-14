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
            _player = new Player(new Point(0,0), resourses.Player, new Rectangle(0, 0, 32, 32));
            _gun = new Gun(_player, resourses.Gun, new Rectangle(0, 0, 0, 0));
            //TODO: отдельный метод здесь AddObjectToMap или типа того, автоматически биндящий Moved на Invoke:
            _player.Moved += () => Update?.Invoke();
            _gun.Moved += () => Update?.Invoke();
            _map.AddObject(_player,false);
            _map.AddObject(_gun, false);
            // ^ ну ты понел
            //а этому парню вообще не суждено звенеть в ивенты:
            _map.AddObject(new Enemy(new Point(64, 64), resourses.Enemy, new Rectangle(0,0,0,0)), false);

            CursorUpdate += point => _gun.Angle = (float)((Math.Atan2(point.Y - _gun.Position.Y, point.X - _gun.Position.X)
                                                    + 2 * Math.PI) * 180 / Math.PI) % 360;
        }

        public Map GetMap()
        {
            return _map;
        }

        public Player GetPlayer()
        {
            return _player;
        }

        public event Action Update;
        private event Action<Point> CursorUpdate;

    }
}