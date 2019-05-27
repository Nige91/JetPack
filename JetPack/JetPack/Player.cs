using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace JetPack
{
	class Player
	{
		private double y;
		private double x;
		private double sizeX;
		private double sizeY;
		private double speed;
		private double minSpeed = -5;
		private double maxSpeed = 5;
		private double gravity = 0.6;
		private double jetPackBoost = 10;
		SKBitmap playerBitmap;

		public Player(float x, float y, float sizeX, float sizeY)
		{
			this.x = x;
			this.y = y;
			this.sizeX = sizeX;
			this.sizeY = sizeY;
			this.speed = 0;
			string resourceID = "JetPack.media.player_up.png";
			playerBitmap = LoadBitmap(resourceID);
		}

		public void Loop()
		{
			this.ApplyGravity();
			this.Move();
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
			if (this.y - this.speed < Globals.yAxisLength - sizeY)
			{
				y -= speed;
			}
			else if (this.y < Globals.yAxisLength - sizeY)
			{
				this.y = Globals.yAxisLength - sizeY;
			}
		}

		public void Touch(object sender, SKTouchEventArgs e)
		{
			this.JetPackBoost();
		}

		public void Draw(SKCanvas canvas)
		{
			canvas.DrawBitmap(playerBitmap, new SKRect((float)x, (float)y, (float)(x + sizeX), (float)(y + sizeY)));
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
