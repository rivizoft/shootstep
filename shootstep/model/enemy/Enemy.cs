using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shootstep
{
    class Enemy : IBaseGameObj
    {
        public static Enemy SpawnEnemy(Enemy sampleEnemy, Point playerPosition, Size windowSize, Point cameraShift)
        {
            var randomizer = new Random();
            var spawnBehindVBorder = randomizer.Next(2) < 1;
            var spawnPositionModifier = randomizer.Next(2) < 1 ? 1 : -1;
            var x = playerPosition.X + spawnPositionModifier * 2 * windowSize.Width;
            var y = playerPosition.Y + spawnPositionModifier * 2 * windowSize.Height;
           // if (spawnBehindVBorder) y += randomizer.Next(-windowSize.Height / 2, windowSize.Height / 2);
            //else x += randomizer.Next(-windowSize.Width / 2, windowSize.Width / 2);
            return new Enemy(new Point(x,y), sampleEnemy.Sprite, sampleEnemy.Bbox,sampleEnemy.SpriteGlow );
        }

        public Point SpeedVector { get; set; }
        public Bitmap Sprite { get; set; }
        public Point Position { get; set; }
        public Rectangle Bbox { get; set; }
        public Bitmap SpriteGlow { get; set; }

        public Enemy(Point position, Bitmap sprite, Rectangle bbox, Bitmap spriteGlow)
        {
            Position = position;
            Sprite = sprite;
            Bbox = bbox;
            SpriteGlow = BlurEffect.Blur(new Bitmap(spriteGlow, sprite.Width + 30, sprite.Height + 30), 10);
        }

        public void MoveTo(Point vector)
        {
            var position = Position;
            position.X += vector.X;
            position.Y += vector.Y;
            Position = position;
        }

        public void Move()
        {
            var p = Globals.GetGlobalInfo().Player.Position;
            var d = new Point(Math.Sign(p.X - Position.X) * 2, Math.Sign(p.Y - Position.Y) * 2);
            MoveTo(d);
        }

        public void InvokeCollision(IBaseGameObj other) => Collision?.Invoke(other);
        public event Action<IBaseGameObj> Collision;
        public event Action Moved;
        
    }
}
