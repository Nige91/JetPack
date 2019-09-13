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
		public float maxHealth { get; private set; }
		public MovementModule movementModule { get; set; }
		public SKBitmap bitmap { get; private set; }
		public WeaponModule weaponModule { get; private set; }

		public float health { get; private set; }

		public Enemy(float maxHealth, MovementModule movementModule, WeaponModule weaponModule, string bitmapResourceId)
		{
			this.maxHealth = maxHealth;
			this.health = maxHealth;
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
			Interface.DrawHealthbar(canvas, movementModule.coords.X, movementModule.coords.Y, health / maxHealth);
		}

		public void SufferDamage(float damage)
		{
			this.health -= damage;
		}

		public bool isDead()
		{
			return health < 0;
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
