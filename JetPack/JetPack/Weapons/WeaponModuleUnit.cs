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
		public float damage { get; private set; }
		public bool friendly { get; set; }
		public MovementModule movementTemplate { get; private set; }
		public string projectileBitmapResourceString { get; private set; }
		public string explosionBitmapResourceString { get; private set; }
		public int explDuration { get; private set; }

		private long cooldownStartTime;
		private bool cooledDown;


		public WeaponModuleUnit(float frequency, float damage, MovementModule movementTemplate, string projectileBitmapResourceString, string explosionBitmapResourceString, int explDuration)
		{
			this.interval = 1000 / frequency;
			this.damage = damage;
			this.movementTemplate = movementTemplate;
			this.projectileBitmapResourceString = projectileBitmapResourceString;
			this.explosionBitmapResourceString = explosionBitmapResourceString;
			this.cooldownStartTime = Helper.GetMilliseconds();
			this.friendly = false;
			this.explDuration = explDuration;
		}

		public void Loop(SKPoint coords, bool active)
		{
			ReduceCooldown();
			if (CooldownReady() && active)
			{
				Shoot(coords);
			}
		}

		public void SetCooldownPhaseShiftPercent(float percent)
		{
			cooldownStartTime -= (long)(interval * percent / 100f);
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
			Projectile projectile = new Projectile(movementTemplate.Copy(coords), projectileBitmapResourceString, explosionBitmapResourceString, damage, explDuration);
			projectile.friendly = this.friendly;
			ProjectileManager.AddProjectile(projectile);
		}
	}
}
