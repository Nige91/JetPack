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
		public SKBitmap explBitmap { get; private set; }
		public WeaponModule weaponModule { get; private set; }
		public int explDuration { get; private set; }
		private bool exploded = false;
		private long explStart;

		public float health { get; private set; }

		public Enemy(float maxHealth, MovementModule movementModule, WeaponModule weaponModule, string bitmapResourceId, string explBitmapResourceId, int explDuration)
		{
			this.maxHealth = maxHealth;
			this.health = maxHealth;
			this.movementModule = movementModule;
			this.weaponModule = weaponModule;
			this.bitmap = Helper.LoadBitmap(bitmapResourceId);
			this.explBitmap = Helper.LoadBitmap(explBitmapResourceId);
			this.explDuration = explDuration;
		}

		public void Loop()
		{
			Move();
			LoopWeapons();
		}

		private void Move()
		{
			if (!exploded)
			{
				movementModule.Move(); 
			}
		}

		private void LoopWeapons()
		{
			weaponModule.Loop(movementModule.coords);
		}

		public void Draw(SKCanvas canvas)
		{
			if (!exploded)
			{
				canvas.DrawBitmap(bitmap, movementModule.GetRect());
				Interface.DrawHealthbar(canvas, movementModule.coords.X, movementModule.coords.Y, health / maxHealth); 
			}
			else
			{
				canvas.DrawBitmap(explBitmap, movementModule.GetRectExpl());
				Interface.DrawHealthbar(canvas, movementModule.coords.X, movementModule.coords.Y, health / maxHealth);
			}
		}

		public void SufferDamage(float damage)
		{
			this.health -= damage;
		}

		public bool IsDead()
		{
			return health <= 0;
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
