using System;
using System.Drawing;

namespace shootstep
{
    public class Game
    {
        //TODO: implement Game as main model controller
        private Map _map;
        private Player _player;
        
        public Game()
        {
            _map = new Map(128,128);
            _player = new Player(new Point(0,0), new Bitmap("Player.png"), new Rectangle(0,0, 32, 32));
            _map.AddObject(_player,false);
        }

        public Map GetMap()
        {
            return _map;
        }

        public event Action Step;

    }
}