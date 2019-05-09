using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using SkiaSharp;

namespace JetPack
{
    class Player
    {
        private float y;
        private float x;
        private float sizeX;
        private float sizeY;
        SKBitmap playerBitmap;

        public Player(float x, float y, float sizeX, float sizeY)
        {
            this.x = x;
            this.y = y;
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            string resourceID = "JetPack.media.player_up.png";
            Assembly assembly = GetType().GetTypeInfo().Assembly;

            using (Stream stream = assembly.GetManifestResourceStream(resourceID))
            {
                playerBitmap = SKBitmap.Decode(stream);
            }
        }
        

        public void Draw(SKCanvas canvas, float surfaceWidth, float surfaceHeight)
        {
            canvas.DrawBitmap(playerBitmap, new SKPoint(x, y));
        }
    }
}
