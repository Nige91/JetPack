using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;

namespace JetPack
{
	static class Interface
	{
		private static float healthBarPosX = 0;
		private static float healthBarPosY = -5;
		private static float healthBarWidth = 10;
		private static float healthBarHeight = 1;

		public static void DrawHealthbar(SKCanvas canvas, float x, float y, float life)
		{
			SKPaint health = new SKPaint
			{
				Style = SKPaintStyle.Fill,
				Color = SKColors.Green
			};

			SKPaint backGround = new SKPaint
			{
				Style = SKPaintStyle.Fill,
				Color = SKColors.Red
			};

			canvas.DrawRect(x + healthBarPosX, y + healthBarPosY, healthBarWidth, healthBarHeight, backGround);
			canvas.DrawRect(x + healthBarPosX, y + healthBarPosY, healthBarWidth * life, healthBarHeight, health);
		}
	}
}
