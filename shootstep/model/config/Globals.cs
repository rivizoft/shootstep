using System;
using System.Drawing;
using shootstep.view;

namespace shootstep.model.config
{
    public class Globals
    {
        private static Globals _instance;

        private readonly Player _player;
        public readonly EnemyOptions Enemy;
        public IBaseGameObj Player => _player;
        public MapOptions MapOptions { get; }
        public Camera View { get; }
        public Size WindowSize { get; set; }
        public readonly Action Update;

        private Globals(int mapWidth, int mapHeight, Camera cam, Player player)
        {
            _player = player;
            MapOptions = new MapOptions {Width = mapWidth, Height = mapHeight};
            Enemy = new EnemyOptions(1000,3);
            View = cam;
            WindowSize = new Size(512,512);
            Update += Enemy.Update;
        }
        
        public static void Init(int mapWidth, int mapHeight, Camera cam, Player player)
        {
            _instance = new Globals(mapWidth, mapHeight, cam, player);
        }
        
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
    }
}