using System;
using System.Collections.Generic;
using System.Text;
using JetPack.Movement;
using SkiaSharp;

namespace JetPack.Weapons
{
	class WeaponModuleUnit
	{
		public float interval { get; private set; }
		public float strength { get; private set; }
		public List<Projectile> projectiles { get; private set; }
		public MovementModule movementTemplate { get; private set; }
		public string projectileBitmapResourceString { get; private set; }

		private float cooldown;
		private long cooldownStartTime;
		private bool cooledDown;


		public WeaponModuleUnit(float frequency, float strength, MovementModule movementTemplate, string projectileBitmapResourceString)
		{
			this.interval = 1000 / frequency;
			this.strength = strength;
			this.projectiles = new List<Projectile>();
			this.movementTemplate = movementTemplate;
			this.projectileBitmapResourceString = projectileBitmapResourceString;
			this.cooldown = 0;
			this.cooldownStartTime = Helper.GetMilliseconds();
		}

		public void Loop(SKPoint coords)
		{
			MoveProjectiles();
			ReduceCooldown();
			if (CooldownReady())
			{
				Shoot(coords);
			}
		}

		public void DrawProjectiles(SKCanvas canvas)
		{
			foreach (var projectile in projectiles)
			{
				projectile.Draw(canvas);
			}
		}

		private void MoveProjectiles()
		{
			foreach (var projectile in projectiles)
			{
				projectile.Move();
			}
		}

		private void ReduceCooldown()
		{
			if(Helper.GetMilliseconds() - cooldownStartTime > interval && Helper.GetMilliseconds() - cooldownStartTime < 2* interval)
			{
				cooledDown = true;
				cooldownStartTime += (long)interval;
			}
			else if(Helper.GetMilliseconds() - cooldownStartTime >= 2 * interval)
			{
				cooledDown = true;
				cooldownStartTime = Helper.GetMilliseconds();
			}
		}

		private bool CooldownReady()
		{
			return cooledDown;
		}

		private void Shoot(SKPoint coords)
		{
			cooledDown = false;
			projectiles.Add( new Projectile(movementTemplate.Copy(coords), projectileBitmapResourceString));
		}


	}
}
