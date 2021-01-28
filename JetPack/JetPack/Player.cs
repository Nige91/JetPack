using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using JetPack.Weapons;
using JetPack.Drawing;

namespace JetPack
{
	class Player
	{
		private SKPoint pos;
		private double sizeX = Settings.Player.sizeX;
		private double sizeY = Settings.Player.sizeY;
		private double explSizeX = Settings.Player.explSizeX;
		private double explSizeY = Settings.Player.explSizeY;
		private double speed;
		private double minSpeed = Settings.Player.minSpeed;
		private double maxSpeed = Settings.Player.maxSpeed;
		private double gravity = Settings.Player.gravity;
		private double jetPackStrength = Settings.Player.jetPackStrength;
		private double health = Settings.Player.maxHealth;
		private double maxHealth = Settings.Player.maxHealth;
		private bool jetPackActive = false;
		private Animator animatorExpl;
		private int explDuration;
		private bool exploded = false;
		private long explStart;
		private SKBitmap playerBitmap;
		private SKBitmap explBitmap;
		private WeaponModule weapon;

		public Player()
		{
			this.pos = new SKPoint(Settings.Player.startPosX, Settings.Player.startPosY);
			this.speed = 0;
			playerBitmap = Helper.LoadBitmap("JetPack.media.player_up.png");
			weapon = WeaponModuleFactory.CreatePlayerWeapon1(
				Settings.Player.Weapon.frequency, 
				Settings.Player.Weapon.damage,
				Settings.Player.Weapon.projSpeed
			);
			weapon.SetFriendly();
			weapon.active = false;
			jetPackActive = false;
			this.animatorExpl = new Animator(
				"JetPack.media.explosions.explosion1_", 
				1, 
				Settings.Player.explAnimStepDuration
			);
			this.explDuration = Settings.Player.explAnimStepDuration * 1;
		}

		public void Loop()
		{
			this.ApplyGravity();
			this.ApplyJetPack();
			this.Move();
			this.weapon.Loop(pos);
		}

		public void TouchLeft()
		{
			this.jetPackActive = true;
		}

		public void ReleaseLeft()
		{
			this.jetPackActive = false;
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
				canvas.DrawBitmap(playerBitmap, GetRect());
				GraphicalUserInterface.DrawHealthbar(canvas, pos.X, pos.Y, (float)(health / maxHealth)); 
			}
			else
			{
				animatorExpl.Draw(canvas, GetRectExpl());
			}
		}

		public void SufferDamage(float damage)
		{
			health -= damage;
			if(health <= 0)
			{
				Explode();
			}
		}

		public SKRect GetRect()
		{
			return new SKRect(pos.X, pos.Y, (float)(pos.X + sizeX), (float)(pos.Y + sizeY));
		}

		public SKRect GetRectExpl()
		{
			return new SKRect(pos.X, pos.Y, (float)(pos.X + explSizeX), (float)(pos.Y + explSizeY));
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
			if (this.speed - this.gravity > this.minSpeed)
			{
				speed -= gravity;
			}
			else if(this.speed > this.minSpeed)
			{
				speed = minSpeed;
			}
		}

		private void ApplyJetPack()
		{
			if (this.jetPackActive)
			{
				if (this.speed + this.jetPackStrength < this.maxSpeed)
				{
					this.speed += this.jetPackStrength;
				}
				else if (this.speed < this.maxSpeed)
				{
					this.speed = this.maxSpeed;
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

		private void Die()
		{
			health = maxHealth;
		}
	}
}
