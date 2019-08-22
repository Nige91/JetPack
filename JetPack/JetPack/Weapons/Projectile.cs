using System;
using System.Collections.Generic;
using System.Text;
using JetPack.Movement;
using SkiaSharp;

namespace JetPack.Weapons
{
	class Projectile
	{
		public MovementModule movementModule { get; set; }
		public SKBitmap bitmap { get; private set; }

		public Projectile(MovementModule movementModule, string bitmapResourceId)
		{
			this.movementModule = movementModule;
			this.bitmap = Helper.LoadBitmap(bitmapResourceId);
		}

		public void Move()
		{
			movementModule.Move();
		}

		public void Draw(SKCanvas canvas)
		{
			canvas.DrawBitmap(bitmap, movementModule.GetRect());
		}
	}
}
