using System;
using System.Drawing;

namespace shootstep
{
    public class Game
    {//TODO: implement Game as main model controller
        private Map _map;
        private Player _player;
        
        public Game()
        {
            _map = new Map(128,128);
            _player = new Player(32,32, new Bitmap(0,0), -8,8,8,8);
            _map.AddObject(_player,false);
        }

        public event Action Step;

    }
}