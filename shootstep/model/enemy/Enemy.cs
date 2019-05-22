using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using shootstep.model.config;

namespace shootstep
{
    class Enemy : IBaseGameObj
    {
        public static Enemy SpawnEnemy(Enemy sampleEnemy, Point playerPosition, Size windowSize, Point cameraShift)
        {
            var randomizer = new Random();
            var spawnBehindVBorder = randomizer.Next(2) < 1;
            var spawnPositionModifier = randomizer.Next(2) < 1 ? 1 : -1;
            var x = playerPosition.X + spawnPositionModifier * windowSize.Width;
            var y = playerPosition.Y + spawnPositionModifier * windowSize.Height;
            sampleEnemy.MoveTo(new Point(x,y));
            return sampleEnemy;
        }

        public Point SpeedVector { get; set; }
        public int Health { get; set; }
        public Bitmap Sprite { get; set; }
        public Point Position { get; set; }
        public Rectangle Bbox { get; set; }
        public Bitmap SpriteGlow { get; set; }

        public Enemy(Point position, Bitmap sprite, Rectangle bbox, Bitmap spriteGlow)
        {
            Position = position;
            Sprite = sprite;
            Bbox = bbox;
            Health = 4;
            SpriteGlow = BlurEffect.Blur(new Bitmap(spriteGlow, sprite.Width + 30, sprite.Height + 30), 10);
            InitCollisions();
        }

        private void InitCollisions()
        {
            Collision += (other) =>
            {
                switch (other)
                {
                    case Player p:
                        p.Health--;
                        SpeedVector = new Point(-2 * SpeedVector.X, -2 * SpeedVector.Y);
                        break;
                    case DubstepHolyRay d:
                        Health--;
                        SpeedVector = new Point(SpeedVector.X / 3, SpeedVector.Y / 3);
                        break;
                    case Enemy e:
                        SpeedVector = new Point(-2 * SpeedVector.X, -2 * SpeedVector.Y);
                        break;
                }
            };
        }

        public void MoveTo(Point vector)
        {
            var position = Position;
            position.X += vector.X;
            position.Y += vector.Y;
            Position = position;
            Moved?.Invoke();
           // Console.WriteLine("Enemy moved to {0},{1}", Position.X, Position.Y);
        }

        public void Move()
        {
            var p = Globals.GetGlobalInfo().Player.Position;
            var d = new Point(Math.Sign(p.X - Position.X) * 2, Math.Sign(p.Y - Position.Y) * 2);
            SpeedVector = d;
            MoveTo(SpeedVector);
        }

        public void InvokeCollision(IBaseGameObj other) => Collision?.Invoke(other);
        public event Action<IBaseGameObj> Collision;
        public event Action Moved;
        
    }
}
