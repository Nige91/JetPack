using System;
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
		private double minSpeed = -5;
		private double maxSpeed = 5;
		private double gravity = 0.6;
		private double jetPackBoost = 10;
		private double health = 80;
		private double maxHealth = 100;
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
			weapon = WeaponModuleFactory.CreateWeapon1(3, 30, 20);
			weapon.active = false;
		}

		public void Loop()
		{
			this.ApplyGravity();
			this.Move();
			this.weapon.Loop(pos);
		}

		public void TouchLeft()
		{
			this.JetPackBoost();
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
			canvas.DrawBitmap(playerBitmap, new SKRect(pos.X, pos.Y, (float)(pos.X + sizeX), (float)(pos.Y + sizeY)));
			Interface.DrawHealthbar(canvas, pos.X, pos.Y, (float)(health / maxHealth));
			weapon.DrawProjectiles(canvas);
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

		private void JetPackBoost()
		{
			if(this.speed + this.jetPackBoost < this.maxSpeed)
			{
				this.speed += this.jetPackBoost;
			}
			else if(this.speed < this.maxSpeed)
			{
				this.speed = this.maxSpeed;
			}
		}

		private void Move()
		{
			if (pos.Y - this.speed < Globals.yAxisLength - sizeY)
			{
				pos.Y -= (float)speed;
			}
			else if (pos.Y < Globals.yAxisLength - sizeY)
			{
				pos.Y = (float)(Globals.yAxisLength - sizeY);
			}
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
