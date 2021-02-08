using JetPack.Drawing;
using JetPack.Weapons;
using SkiaSharp;

namespace JetPack
{
	class Player
	{
		private SKPoint pos;
		private float sizeX = Settings.Player.sizeX;
		private float sizeY = Settings.Player.sizeY;
		private float explSizeX = Settings.Player.explSizeX;
		private float explSizeY = Settings.Player.explSizeY;
		private float jetPackFlameSizeX = Settings.Player.jetPackFlameSizeX;
		private float jetPackFlameSizeY = Settings.Player.jetPackFlameSizeY;
		private float jetPackFlamePosX = Settings.Player.jetPackFlamePosX;
		private float jetPackFlamePosY = Settings.Player.jetPackFlameSizeY;
		private float speed;
		private float minSpeed = Settings.Player.minSpeed;
		private float maxSpeed = Settings.Player.maxSpeed;
		private float gravity = Settings.Player.gravity;
		private float jetPackStrength = Settings.Player.jetPackStrength;
		private float health = Settings.Player.maxHealth;
		private float maxHealth = Settings.Player.maxHealth;
		private bool jetPackActive = false;
		private Animator animatorExpl;
		private Animator animatorJetPack;
		private int explDuration;
		private bool exploded = false;
		private long explStart;
		private SKBitmap playerBitmapUp;
		private SKBitmap playerBitmapDown;
		private SKBitmap playerBitmapNeutral;
		private WeaponModuleFactory weaponModuleFactory;
		private WeaponModule weapon;

		public Player()
		{
			pos = new SKPoint(
				Settings.Player.startPosX,
				Settings.Player.startPosY
			);
			speed = 0;
			playerBitmapUp = Helper.LoadBitmap("JetPack.media.player.up.png");
			playerBitmapDown = Helper.LoadBitmap("JetPack.media.player.down.png");
			playerBitmapNeutral = Helper.LoadBitmap("JetPack.media.player.neutral.png");
			weaponModuleFactory = WeaponModuleFactory.GetInstance();
			weapon = weaponModuleFactory.CreatePlayerWeapon(
				Settings.Player.Weapon.frequency,
				Settings.Player.Weapon.damage,
				Settings.Player.Weapon.projSpeed
			);
			weapon.SetFriendly();
			weapon.active = false;
			jetPackActive = false;
			animatorExpl = new Animator(
				"JetPack.media.explosions.explosion1_",
				1,
				Settings.Player.explAnimStepDuration
			);
			explDuration = Settings.Player.explAnimStepDuration * 1;
			animatorJetPack = new Animator(
				"JetPack.media.fire.fire_start_",
				"JetPack.media.fire.fire_hold_",
				"JetPack.media.fire.fire_stop_",
				8,
				23,
				19,
				Settings.Player.jetPackAnimStepDuration
			);
		}

		public void Loop()
		{
			ApplyGravity();
			ApplyJetPack();
			Move();
			weapon.Loop(pos);
		}

		//TODO move to different function
		public void TouchLeft()
		{
			jetPackActive = true;
			animatorJetPack.Start();
		}

		public void ReleaseLeft()
		{
			jetPackActive = false;
			animatorJetPack.Stop();
		}

		public void TouchRight()
		{
			weapon.active = true;
		}

		public void ReleaseRight()
		{
			weapon.active = false;
		}

		public void Draw(SKCanvas canvas)
		{
			if (!exploded)
			{
				canvas.DrawBitmap(ChooseBitmap(), GetRect());
				GraphicalUserInterface.DrawHealthbar(
					canvas,
					pos.X,
					pos.Y,
					(float)(health / maxHealth)
				);
				animatorJetPack.Draw(canvas, GetRectJetPackFlame());
			}
			else
			{
				animatorExpl.Draw(canvas, GetRectExpl());
			}
		}

		public void SufferDamage(float damage)
		{
			health -= damage;
			if (health <= 0)
			{
				Explode();
			}
		}

		public SKRect GetRect()
		{
			return new SKRect(
				pos.X,
				pos.Y,
				pos.X + sizeX,
				pos.Y + sizeY
			);
		}

		private SKRect GetRectExpl()
		{
			return new SKRect(
				pos.X,
				pos.Y,
				pos.X + explSizeX,
				pos.Y + explSizeY
			);
		}

		private SKRect GetRectJetPackFlame()
		{
			return new SKRect(
				pos.X + jetPackFlamePosX,
				pos.Y + jetPackFlamePosY,
				pos.X + jetPackFlamePosX + jetPackFlameSizeX,
				pos.Y + jetPackFlamePosY + jetPackFlameSizeY
			);
		}

		public bool IsGameOver()
		{
			return exploded && ExplosionFinished();
		}

		private void Explode()
		{
			exploded = true;
			explStart = Helper.GetMilliseconds();
		}

		private bool ExplosionFinished()
		{
			return Helper.GetMilliseconds() - explStart > explDuration;
		}

		private void ApplyGravity()
		{
			if (speed - gravity > minSpeed)
			{
				speed -= gravity;
			}
			else if (speed > minSpeed)
			{
				speed = minSpeed;
			}
		}

		private void ApplyJetPack()
		{
			if (jetPackActive)
			{
				if (speed + jetPackStrength < maxSpeed)
				{
					speed += jetPackStrength;
				}
				else if (speed < maxSpeed)
				{
					speed = maxSpeed;
				}
			}
		}

		private void Move()
		{
			pos.Y -= (float)speed;
			if (pos.Y < 0)
			{
				pos.Y = 0;
			}
			else if (pos.Y > Settings.General.yAxisLength - sizeY)
			{
				pos.Y = (float)(Settings.General.yAxisLength - sizeY);
			}
		}

		private SKBitmap ChooseBitmap()
		{
			if (jetPackActive && speed > 0)
			{
				return playerBitmapUp;
			}
			else if (jetPackActive && speed <= 0 || !jetPackActive && speed > 0)
			{
				return playerBitmapNeutral;
			}
			else
			{
				return playerBitmapDown;
			}
		}
	}
}
