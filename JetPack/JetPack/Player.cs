﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using JetPack.Weapons;

namespace JetPack
{
	class Player
	{
		//TODO remove magic numbers
		private SKPoint pos;
		private double sizeX;
		private double sizeY;
		private double speed;
		private double minSpeed = Settings.Player.minSpeed;
		private double maxSpeed = Settings.Player.maxSpeed;
		private double gravity = Settings.Player.gravity;
		private double jetPackStrength = Settings.Player.jetPackStrength;
		private double health = Settings.Player.maxHealth;
		private double maxHealth = Settings.Player.maxHealth;
		private bool jetPackActive = false;
		SKBitmap playerBitmap;
		WeaponModule weapon;

		public Player(float x, float y, float sizeX, float sizeY)
		{
			this.pos = new SKPoint(x, y);
			this.sizeX = sizeX;
			this.sizeY = sizeY;
			this.speed = 0;
			string resourceID = "JetPack.media.player_up.png";
			playerBitmap = LoadBitmap(resourceID);
			weapon = WeaponModuleFactory.CreatePlayerWeapon1(
				Settings.Player.Weapon.frequency, 
				Settings.Player.Weapon.damage,
				Settings.Player.Weapon.projSpeed
			);
			weapon.SetFriendly();
			weapon.active = false;
			jetPackActive = false;
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
			canvas.DrawBitmap(playerBitmap, GetRect());
			Interface.DrawHealthbar(canvas, pos.X, pos.Y, (float)(health / maxHealth));
		}

		public void SufferDamage(float damage)
		{
			health -= damage;
		}

		public SKRect GetRect()
		{
			return new SKRect(pos.X, pos.Y, (float)(pos.X + sizeX), (float)(pos.Y + sizeY));
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

		private SKBitmap LoadBitmap(string resourceId)
		{
			string resourceID = "JetPack.media.player_up.png";
			Assembly assembly = GetType().GetTypeInfo().Assembly;

			using (Stream stream = assembly.GetManifestResourceStream(resourceID))
			{
				return SKBitmap.Decode(stream);
			}
		}
	}
}
