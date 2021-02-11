using SkiaSharp;

namespace JetPack
{
	//TODO make Singleton instead of static
	static class GraphicalUserInterface
	{
		private static float healthBarPosX = Settings.Interface.healthBarPosX;
		private static float healthBarPosY = Settings.Interface.healthBarPosY;
		private static float healthBarWidth = Settings.Interface.healthBarWidth;
		private static float healthBarHeight = Settings.Interface.healthBarHeight;

		private static float scoreTextSize = Settings.Interface.scoreTextSize;
		private static float scoreTextPosX = Settings.Interface.scoreTextPosX;
		private static float scoreTextPosY = Settings.Interface.scoreTextPosY;

		private static float fpsTextSize = Settings.Interface.fpsTextSize;
		private static float fpsTextPosX = Settings.Interface.fpsTextPosX;
		private static float fpsTextPosY = Settings.Interface.fpsTextPosY;

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

			canvas.DrawRect(
				x + healthBarPosX,
				y + healthBarPosY,
				healthBarWidth,
				healthBarHeight,
				backGround
			);
			canvas.DrawRect(
				x + healthBarPosX,
				y + healthBarPosY,
				healthBarWidth * life,
				healthBarHeight,
				health
			);
		}

		public static void DrawScore(SKCanvas canvas, int score)
		{
			SKPaint paint = new SKPaint
			{
				Style = SKPaintStyle.Fill,
				Color = SKColors.White,
				TextSize = scoreTextSize,
				IsAntialias = true
			};

			canvas.DrawText(
				"Score: " + score.ToString(),
				new SKPoint(scoreTextPosX, scoreTextPosY),
				paint
			);
		}

		public static void DrawFPS(SKCanvas canvas, int fps)
		{
			SKPaint paint = new SKPaint
			{
				Style = SKPaintStyle.Fill,
				Color = SKColors.White,
				TextSize = fpsTextSize,
				IsAntialias = true
			};

			canvas.DrawText(
				"FPS: " + fps.ToString(),
				new SKPoint(fpsTextPosX, fpsTextPosY),
				paint
			);
		}
	}
}
