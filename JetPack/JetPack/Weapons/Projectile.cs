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
		public SKBitmap projBitmap { get; private set; }
		public SKBitmap explBitmap { get; private set; }
		public float damage { get; set; }
		public int explDuration { get; private set; }
		private bool exploded = false;
		private long explStart;

		public Projectile(MovementModule movementModule, string projBitmapResourceId, string explBitmapResourceId, float damage, int explDuration)
		{
			this.movementModule = movementModule;
			this.projBitmap = Helper.LoadBitmap(projBitmapResourceId);
			this.explBitmap = Helper.LoadBitmap(explBitmapResourceId);
			this.damage = damage;
			this.explDuration = explDuration;
			this.friendly = false;
		}

		public void Move()
		{
			if (!exploded)
			{
				movementModule.Move();
			}
		}

		public void Draw(SKCanvas canvas)
		{
			if (!exploded)
			{
				canvas.DrawBitmap(projBitmap, movementModule.GetRect()); 
			}
			else
			{
				canvas.DrawBitmap(explBitmap, movementModule.GetRectExpl());
			}
		}

		public void Explode()
		{
			exploded = true;
			explStart = Helper.GetMilliseconds();
		}

		public bool ExplosionFinished()
		{
			return Helper.GetMilliseconds() - explStart > explDuration;
		}
	}
}
