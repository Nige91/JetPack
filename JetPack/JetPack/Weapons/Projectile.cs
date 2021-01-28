using System;
using System.Collections.Generic;
using System.Text;
using JetPack.Movement;
using JetPack.Drawing;
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
		private Animator animatorExpl;
		private int explDuration;
		private bool exploded = false;
		private long explStart;

		public Projectile(
			MovementModule movementModule, 
			string projBitmapResourceId,
			string explAnimResString,
			int explAnimNSteps,
			int explAnimStepDuration,
			float damage
		)
		{
			this.movementModule = movementModule;
			this.projBitmap = Helper.LoadBitmap(projBitmapResourceId);
			this.animatorExpl = new Animator(explAnimResString, explAnimNSteps, explAnimStepDuration);
			this.explDuration = explAnimStepDuration * explAnimNSteps;
			this.damage = damage;
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
				animatorExpl.Draw(canvas, movementModule.GetRectExpl());
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
