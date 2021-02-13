using JetPack.Drawing;
using JetPack.Movement;
using JetPack.Weapons;
using JetPack.Timing;
using SkiaSharp;

namespace JetPack.Enemies
{
	class Enemy
	{
		public float maxHealth { get; private set; }
		public float health { get; private set; }
		public MovementModule movementModule { get; private set; }
		public SKBitmap explBitmap { get; private set; }
		public WeaponModule weaponModule { get; private set; }
		public int explDuration { get; private set; }

		private Animator animatorNormal;
		private Animator animatorExpl;
		private bool exploded = false;
		private long explStart;

		private LoopTimer loopTimer;


		public Enemy(
			float maxHealth,
			MovementModule movementModule,
			WeaponModule weaponModule,
			Animator animatorNormal,
			Animator animatorExpl
		)
		{
			this.maxHealth = maxHealth;
			health = maxHealth;
			this.movementModule = movementModule;
			this.weaponModule = weaponModule;
			this.animatorNormal = animatorNormal;
			this.animatorExpl = animatorExpl;
			explDuration = animatorExpl.GetLoopDuration();
			loopTimer = LoopTimer.GetInstance();
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
				GraphicalUserInterface.DrawHealthbar(
					canvas,
					movementModule.coords.X,
					movementModule.coords.Y,
					health / maxHealth
				);
			}
			else
			{
				animatorExpl.Draw(canvas, movementModule.GetRectExpl());
			}
		}

		public void SufferDamage(float damage)
		{
			health -= damage;
		}

		public bool IsDead()
		{
			return health <= 0;
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
	}
}
