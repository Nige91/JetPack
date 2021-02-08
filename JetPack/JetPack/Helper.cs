using SkiaSharp;
using System;
using System.IO;
using System.Reflection;

namespace JetPack
{
	static class Helper
	{
		static Random rand = new Random();

		public static long GetMilliseconds()
		{
			return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
		}

		public static float GetRandomFloat(float min, float max)
		{
			return min + (float)rand.NextDouble() * (max - min);
		}

		public static SKBitmap LoadBitmap(string resourceId)
		{

			Assembly assembly = typeof(App).GetTypeInfo().Assembly;
			using (Stream stream = assembly.GetManifestResourceStream(resourceId))
			{
				return SKBitmap.Decode(stream);
			}
		}

		public static SKPoint Rotate(SKPoint point, float angle)
		{
			SKPoint rot = new SKPoint();
			float angleRad = angle * (float)Math.PI / 180;
			rot.X = (float)(Math.Cos(angleRad) * point.X - Math.Sin(angleRad) * point.Y);
			rot.Y = (float)(Math.Cos(angleRad) * point.Y + Math.Sin(angleRad) * point.X);
			return rot;
		}
	}
}
