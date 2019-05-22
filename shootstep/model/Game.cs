using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using shootstep.model.config;

namespace shootstep
{
    public class Game
    {
        //TODO: implement Game as main model controller
        private Timer _timer;
        private Map _map;
        private Player _player;
        private Gun _gun;
        private Dust[] _dust;
        private Point _cursorPosition;
        private Globals _globalConfig;
        
        public Point CursorPosition
        {
            get => _cursorPosition;
            set
            {
                _cursorPosition = value; 
                CursorUpdate?.Invoke(value);
            }
        }
        
        public Game()
        {
            _globalConfig = Globals.GetGlobalInfo();
            _map = new Map(_globalConfig.MapOptions.Width, _globalConfig.MapOptions.Height);

            _player = new Player(new Point(0,0), 
                resourses.Player, 
                new Rectangle(0, 0, 32, 32), 
                resourses.Player);
            _gun = new Gun(_player, resourses.Gun, 
                new Rectangle(0, 0, 0, 0), 
                resourses.Gun);

            AddToMap(_player, _gun);

            CursorUpdate += point => _gun.Angle = (float)((Math.Atan2(point.Y - _gun.Position.Y, point.X - _gun.Position.X)
                                                    + 2 * Math.PI) * 180 / Math.PI) % 360;
            Update += () => _map.Update();
            Update += TryAddEnemy;
            Update += () => _globalConfig.Update.Invoke();

            _timer = new Timer {Interval = 1};
            _timer.Start();
            _timer.Tick += (x, y) => Update?.Invoke();
        }

        public Globals GetGlobalOptions() => _globalConfig;
        
        public Map GetMap()
        {
            return _map;
        }

        public Player GetPlayer()
        {
            return _player;
        }

        public Dust[] GetDust()
        {
            return _dust;
        }

        public void AddToMap(params IBaseGameObj[] gameObjects)
        {
            foreach (var o in gameObjects)
            {
                //o.Moved += () => Update?.Invoke();
                _map.AddObject(o, false);
            }
        }

        public void TryAddEnemy()
        {
            var e = Globals.GetGlobalInfo();
            if (e.Enemy.SpawnGranted)
            {
                AddToMap(Enemy.SpawnEnemy(new Enemy(Point.Empty, resourses.Enemy,
                        new Rectangle(Point.Empty, resourses.Enemy.Size), resourses.Enemy),
                    _player.Position, e.WindowSize, e.View.GetViewPoint()));
            }

            if (Globals.GetGlobalInfo().SoundContainer.SoundOut.Volume > 1)
            {
                var bullet = new DubstepHolyRay(_globalConfig.Player.Position, resourses.bullet, 
                    new Rectangle(_globalConfig.Player.Position, resourses.bullet.Size), resourses.bullet,
                    new Point((CursorPosition.X - _gun.Position.X) / 10, (CursorPosition.Y - _gun.Position.Y) / 10));
            }
        }

        public void AddDust()
        {
            var random = new Random();
            var countEllipses = random.Next(500, 1000);
            _dust = new Dust[countEllipses];

            for (var i = 0; i < countEllipses; i++)
            {
                var size = random.Next(1, 8);
                var pos = new Point(random.Next(-_globalConfig.MapOptions.Width, _globalConfig.MapOptions.Width), 
                    random.Next(-_globalConfig.MapOptions.Height, _globalConfig.MapOptions.Height));
                _dust[i] = new Dust(pos, new Bitmap(resourses.Dust, size, size),
                    new Rectangle(pos.X, pos.Y, size, size));
                AddToMap(_dust[i]);
            }
        }

        public event Action Update;
        private event Action<Point> CursorUpdate;

    }
}