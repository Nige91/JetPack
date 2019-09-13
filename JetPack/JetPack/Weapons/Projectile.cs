using System;
using System.Collections.Generic;
using System.Text;
using JetPack.Movement;
using SkiaSharp;

namespace JetPack.Weapons
{
	class Projectile
	{
		public bool friendly { get; set; }
		public MovementModule movementModule { get; set; }
		public SKBitmap bitmap { get; private set; }
		public float damage { get; set; }

		public Projectile(MovementModule movementModule, string bitmapResourceId, float damage)
		{
			this.movementModule = movementModule;
			this.bitmap = Helper.LoadBitmap(bitmapResourceId);
			this.damage = damage;
			this.friendly = false;
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
