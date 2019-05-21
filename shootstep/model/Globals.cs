using System;
using System.Data;
using System.Drawing;
using shootstep.view;

namespace shootstep
{
    public class Globals
    {
        private static Globals _instance;

        public static Globals GetGlobalInfo()
        {
            if (_instance is null)
            {
                var player = new Player(Point.Empty, resourses.Player,
                    new Rectangle(Point.Empty, resourses.Player.Size), resourses.PlayerLight);
                Init(512,512, new Camera(player, 512, 512), player);
                Console.WriteLine("Global config hasn't been initialised; using defaults");
            }
            return _instance;
        }

        private readonly MapOptions _mapOptions;
        public Size WindowSize { get; set; }
        private Camera _cam;
        public readonly EnemyOptions Enemy;
        private Player _player;
        public IBaseGameObj Player => _player;

        public static void Init(int mapWidth, int mapHeight, Camera cam, Player player)
        {
            _instance = new Globals(mapWidth, mapHeight, cam, player);
        }
        
        private Globals(int mapWidth, int mapHeight, Camera cam, Player player)
        {
            _mapOptions = new MapOptions();
            _mapOptions.Width = mapWidth;
            _mapOptions.Height = mapHeight;
            _player = player;
            Enemy = new EnemyOptions(20,3);
            _cam = cam;
            WindowSize = new Size(512,512);
            Update += Enemy.Update;
        }

        public MapOptions GetMapOptions() => _mapOptions;

        public Camera View => _cam;

        public Action Update;
        
        public class MapOptions
        {
            public int Width { get; set; }
            public int Height { get; set; }
        }

        public class EnemyOptions
        {
            private int _count;
            public int Count 
            { 
                get => _count;
                set
                {
                    _count = value;
                    _cooldown = _cooldownDefaultDelta;
                }
            }
            public int MaxEnemy { get; set; }
            private int _cooldown;
            private int _cooldownDefaultDelta;
            public bool SpawnGranted => _cooldown == 0 && Count < MaxEnemy;

            public EnemyOptions(int cooldown, int maxEnemyCount)
            {
                _cooldownDefaultDelta = cooldown;
                MaxEnemy = maxEnemyCount;
            }

            public void Update()
            {
                if (_cooldown > 0) _cooldown--;
            }
        }
    }
}