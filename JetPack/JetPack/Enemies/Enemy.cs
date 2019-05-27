using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using JetPack.Movement;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace JetPack.Enemies
{
	class Enemy
	{
		public float life { get; private set; }
		public MovementModule movementModule { get; set; }
		public SKBitmap bitmap { get; private set; }

		public Enemy(float life, MovementModule movementModule, string bitmapResourceId)
		{
			this.life = life;
			this.movementModule = movementModule;
			this.bitmap = LoadBitmap(bitmapResourceId);
		}

		public void Loop()
		{
			Move();
		}

		private void Move()
		{
			movementModule.Move();
		}

		public void Draw(SKCanvas canvas)
		{
			canvas.DrawBitmap(bitmap, movementModule.GetRect());
		}

		public bool isDead()
		{
			return life < 0;
		}

		private SKBitmap LoadBitmap(string resourceId)
		{
			Assembly assembly = GetType().GetTypeInfo().Assembly;
			using (Stream stream = assembly.GetManifestResourceStream(resourceId))
			{
				return SKBitmap.Decode(stream);
			}
		}
	}
}
