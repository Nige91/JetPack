using JetPack.Drawing;
using JetPack.Movement;
using JetPack.Timing;
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
		private LoopTimer loopTimer;

		public Projectile(
			MovementModule movementModule,
			SKBitmap projBitmap,
			Animator animatorExpl,
			float damage
		)
		{
			this.movementModule = movementModule;
			this.projBitmap = projBitmap; 
			this.animatorExpl = animatorExpl;
			explDuration = animatorExpl.GetLoopDuration();
			this.damage = damage;
			friendly = false;
			loopTimer = LoopTimer.GetInstance();
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
			explStart = loopTimer.GetTotalMs();
		}

		public bool ExplosionFinished()
		{
			return loopTimer.GetTotalMs() - explStart > explDuration;
		}

		public bool IsOutOfBounds()
		{
			if (
				movementModule.coords.X < 0 ||
				movementModule.coords.Y < 0 ||
				movementModule.coords.X > Settings.General.xAxisLength ||
				movementModule.coords.Y > Settings.General.yAxisLength
			)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
