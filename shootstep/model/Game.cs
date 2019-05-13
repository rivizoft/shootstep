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
        
        public Game()
        {
            _map = new Map(128, 128);
            _player = new Player(new Point(0,0), resourses.Player, new Rectangle(0, 0, 32, 32));
            _map.AddObject(_player,false);
            _gun = new Gun(_player.Position, resourses.Gun, new Rectangle(0, 0, 0, 0));
            _map.AddObject(_gun, false);
            _player.MovePlayer += () => Update.Invoke();
            _gun.MoveGun += () => Update.Invoke();
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

    }
}