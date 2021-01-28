using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using JetPack.Movement;
using JetPack.Weapons;
using JetPack.Drawing;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace JetPack.Enemies
{
	class Enemy
	{
		public float maxHealth { get; private set; }
		public MovementModule movementModule { get; private set; }
		public SKBitmap explBitmap { get; private set; }
		public WeaponModule weaponModule { get; private set; }
		public int explDuration { get; private set; }

		private Animator animatorNormal;
		private Animator animatorExpl;
		private bool exploded = false;
		private long explStart;

		public float health { get; private set; }

		public Enemy(
			float maxHealth, 
			MovementModule movementModule, 
			WeaponModule weaponModule, 
			string normalAnimResString, 
			int normalAnimNSteps, 
			int normalAnimStepDuration, 
			string explAnimResString,
			int explAnimNSteps,
			int explAnimStepDuration
		)
		{
			this.maxHealth = maxHealth;
			this.health = maxHealth;
			this.movementModule = movementModule;
			this.weaponModule = weaponModule;
			this.animatorNormal = new Animator(normalAnimResString, normalAnimNSteps, normalAnimStepDuration);
			this.animatorExpl = new Animator(explAnimResString, explAnimNSteps, explAnimStepDuration);
			this.explDuration = explAnimStepDuration*explAnimNSteps;
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
				animatorNormal.Draw(canvas, movementModule.GetRect());
				GraphicalUserInterface.DrawHealthbar(canvas, movementModule.coords.X, movementModule.coords.Y, health / maxHealth); 
			}
			else
			{
				animatorExpl.Draw(canvas, movementModule.GetRectExpl());
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
