using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using JetPack.Movement;
using JetPack.Weapons;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace JetPack.Enemies
{
	class Enemy
	{
		public float life { get; private set; }
		public MovementModule movementModule { get; set; }
		public SKBitmap bitmap { get; private set; }
		public WeaponModule weaponModule { get; private set; }

		public Enemy(float life, MovementModule movementModule, WeaponModule weaponModule, string bitmapResourceId)
		{
			this.life = life;
			this.movementModule = movementModule;
			this.weaponModule = weaponModule;
			this.bitmap = LoadBitmap(bitmapResourceId);
		}

		public void Loop()
		{
			Move();
			LoopWeapons();
		}

		private void Move()
		{
			movementModule.Move();
		}

		private void LoopWeapons()
		{
			weaponModule.Loop(movementModule.coords);
		}

		public void DrawEnemy(SKCanvas canvas)
		{
			canvas.DrawBitmap(bitmap, movementModule.GetRect());
		}

		public void DrawProjectiles(SKCanvas canvas)
		{
			weaponModule.DrawProjectiles(canvas);
		}

		public bool isDead()
		{
			return life < 0;
		}

		private SKBitmap LoadBitmap(string resourceId)
		{
			return Helper.LoadBitmap(resourceId);
			//Assembly assembly = GetType().GetTypeInfo().Assembly;
			//using (Stream stream = assembly.GetManifestResourceStream(resourceId))
			//{
			//	return SKBitmap.Decode(stream);
			//}
		}
	}
}
