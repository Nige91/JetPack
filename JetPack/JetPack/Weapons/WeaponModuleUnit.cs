﻿using System;
using System.Collections.Generic;
using System.Text;
using JetPack.Movement;
using SkiaSharp;

namespace JetPack.Weapons
{
	class WeaponModuleUnit
	{
		public float shootInterval { get; private set; }
		public float damage { get; private set; }
		public bool friendly { get; set; }
		public MovementModule movementTemplate { get; set; }
		public string projectileBitmapResourceString { get; private set; }

		private string explAnimResString;
		private int explAnimNSteps;
		private int explAnimStepDuration;
		private long cooldownStartTime;
		private bool cooledDown;
		private bool rotating = false;
		private float rotAngleMin = 0;
		private float rotAngleMax = 0;
		private float rotCycleDuration = 0;
		private long rotStartTime = 0;


		public WeaponModuleUnit(
			float frequency, 
			float damage, 
			MovementModule movementTemplate, 
			string projectileBitmapResourceString,
			string explAnimResString,
			int explAnimNSteps,
			int explAnimStepDuration
		)
		{
			this.shootInterval = 1000 / frequency;
			this.damage = damage;
			this.movementTemplate = movementTemplate;
			this.projectileBitmapResourceString = projectileBitmapResourceString;
			this.explAnimResString = explAnimResString;
			this.explAnimNSteps = explAnimNSteps;
			this.explAnimStepDuration = explAnimStepDuration;
			this.cooldownStartTime = Helper.GetMilliseconds();
			this.friendly = false;
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
			cooldownStartTime -= (long)(shootInterval * percent / 100f);
		}

		public void SetRotating(float angleMin, float angleMax, float cycleDuration)
		{
			this.rotating = true;
			this.rotAngleMin = angleMin;
			this.rotAngleMax = angleMax;
			this.rotCycleDuration = cycleDuration * Settings.General.normalTimeUnitInMs;
			this.rotStartTime = Helper.GetMilliseconds();
		}

		private float GetRotationFactor()
		{
			int timePassed = (int)(Helper.GetMilliseconds() - rotStartTime);
			float rotPhase = timePassed % rotCycleDuration;
			return Math.Abs(-1.0f + 2.0f * rotPhase / rotCycleDuration);
		}

		private float GetRotationAngle()
		{
			float factor = GetRotationFactor();
			return rotAngleMin + factor * (rotAngleMax - rotAngleMin);
		}

		private void ReduceCooldown()
		{
			if(Helper.GetMilliseconds() - cooldownStartTime > shootInterval && 
				Helper.GetMilliseconds() - cooldownStartTime < 2* shootInterval
			)
			{
				cooledDown = true;
				cooldownStartTime += (long)shootInterval;
			}
			else if(Helper.GetMilliseconds() - cooldownStartTime >= 2 * shootInterval)
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
			if(rotating)
				movementTemplate.rotation = GetRotationAngle();
			cooledDown = false;
			Projectile projectile = new Projectile(
				movementTemplate.Copy(coords), 
				projectileBitmapResourceString, 
				explAnimResString, 
				explAnimNSteps, 
				explAnimStepDuration, 
				damage
			);
			projectile.friendly = this.friendly;
			ProjectileManager.AddProjectile(projectile);
		}
	}
}
