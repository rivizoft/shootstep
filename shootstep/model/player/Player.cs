using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using shootstep.model.config;

namespace shootstep
{
    public class Player : IBaseGameObj
    {
        public Point SpeedVector { get; set; }
        public int Health { get; set; }

        public Bitmap Sprite { get; set; }
        public Point Position { get; set; }
        public Rectangle Bbox { get; set; }
        public Bitmap SpriteGlow { get; set; }

        public Player(Point position, Bitmap sprite, Rectangle bbox, Bitmap spriteGlow)
        {
            Position = position;
            Sprite = sprite;
            Bbox = bbox;
            Health = 100;
            //1 - степень размытия
            SpriteGlow = new Bitmap(BlurEffect.Blur(spriteGlow, 1), sprite.Width + 30, sprite.Height + 30);
            Collision += (other) => SpeedVector = new Point(SpeedVector.X / 2, SpeedVector.Y / 2);
        }
        
        public Player(int x, int y, Bitmap sprite, int bboxX, int bboxY, 
            int bboxWidth, int bboxHeigth, Bitmap spriteGlow) 
            : this (new Point(x,y), sprite, new Rectangle(bboxX, bboxY, bboxWidth, bboxHeigth), spriteGlow) {}

        public void MoveTo(Point vector)
        {
            Moved?.Invoke();
            var position = Position;
            position.X += vector.X;
            position.Y += vector.Y;
            Position = position;
            Moved?.Invoke();
        }

        private void UpdatePosition()
        {
            if (SpeedVector.IsEmpty) return;
            var position = Position;
            position.X += SpeedVector.X / 3;
            position.X %= Globals.GetGlobalInfo().MapOptions.Width;
            position.Y += SpeedVector.Y / 3;
            position.Y %= Globals.GetGlobalInfo().MapOptions.Height;
            Position = position;
            Moved?.Invoke();
        }

        public void Move()
        {
            UpdatePosition();
            var speed = SpeedVector;
            speed.X = speed.X * 15 / 16;
            speed.Y = speed.Y * 15 / 16;
            SpeedVector = speed;
        }
        

        public event Action<IBaseGameObj> Collision;
        public event Action Moved;

        public void InvokeCollision(IBaseGameObj other) => Collision?.Invoke(other);
    }

    class BlurEffect
    {
        public static Bitmap Blur(Bitmap image, Int32 blurSize)
        {
            return Blur(image, new Rectangle(0, 0, image.Width, image.Height), blurSize);
        }

        private static Bitmap Blur(Bitmap image, Rectangle rectangle, Int32 blurSize)
        {
            Bitmap blurred = new Bitmap(image.Width, image.Height);

            // make an exact copy of the bitmap provided
            using (Graphics graphics = Graphics.FromImage(blurred))
                graphics.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height),
                    new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);

            // look at every pixel in the blur rectangle
            for (int xx = rectangle.X; xx < rectangle.X + rectangle.Width; xx++)
            {
                for (int yy = rectangle.Y; yy < rectangle.Y + rectangle.Height; yy++)
                {
                    int avgR = 0, avgG = 0, avgB = 0;
                    int blurPixelCount = 0;

                    // average the color of the red, green and blue for each pixel in the
                    // blur size while making sure you don't go outside the image bounds
                    for (int x = xx; (x < xx + blurSize && x < image.Width); x++)
                    {
                        for (int y = yy; (y < yy + blurSize && y < image.Height); y++)
                        {
                            Color pixel = blurred.GetPixel(x, y);

                            avgR += pixel.R;
                            avgG += pixel.G;
                            avgB += pixel.B;

                            blurPixelCount++;
                        }
                    }

                    avgR = avgR / blurPixelCount;
                    avgG = avgG / blurPixelCount;
                    avgB = avgB / blurPixelCount;

                    // now that we know the average for the blur size, set each pixel to that color
                    for (int x = xx; x < xx + blurSize && x < image.Width && x < rectangle.Width; x++)
                        for (int y = yy; y < yy + blurSize && y < image.Height && y < rectangle.Height; y++)
                            blurred.SetPixel(x, y, Color.FromArgb(avgR, avgG, avgB));
                }
            }

            return blurred;
        }
    }
}
