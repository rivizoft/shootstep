﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shootstep
{
    public class Gun : IBaseGameObj
    {
        public Point SpeedVector { get; set; }
        public int Health { get; set; }
        public Bitmap Sprite { get; set; }
        private float _angle;
        public Point Position { get; set; }
        public Rectangle Bbox { get; set; }
        public Bitmap SpriteGlow { get; set; }
        private IBaseGameObj _bindPlayer;

        public float Angle
        {
            get => _angle;
            set
            {
                _angle = value;
                Moved?.Invoke();
            }
        }

        public Gun(IBaseGameObj bindPlayer, Bitmap sprite, Rectangle bbox, Bitmap spriteGlow)
        {
            _bindPlayer = bindPlayer;
            Position = bindPlayer.Position;
            Sprite = sprite;
            Bbox = bbox;
            Health = _bindPlayer.Health;
            Angle = 0;
            SpriteGlow = spriteGlow;
        }

        public void MoveTo(Point vector)
        {
            Position = vector;
        }

        public void Move()
        {
            Health = _bindPlayer.Health;
            Position = _bindPlayer.Position;
        }

        public void InvokeCollision(IBaseGameObj other) => Collision?.Invoke(other);
        public event Action<IBaseGameObj> Collision;
        public event Action Moved;
    }
}
