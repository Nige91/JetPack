using SkiaSharp;

namespace JetPack
{
	static class GraphicalUserInterface
	{
		private static float healthBarPosX = Settings.Interface.healthBarPosX;
		private static float healthBarPosY = Settings.Interface.healthBarPosY;
		private static float healthBarWidth = Settings.Interface.healthBarWidth;
		private static float healthBarHeight = Settings.Interface.healthBarHeight;

		private static float scoreTextSize = Settings.Interface.scoreTextSize;
		private static float scoreTextPosX = Settings.Interface.scoreTextPosX;
		private static float scoreTextPosY = Settings.Interface.scoreTextPosY;

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
	}
}
