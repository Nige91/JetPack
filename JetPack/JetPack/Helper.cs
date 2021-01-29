using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System.Runtime.Serialization.Formatters.Binary;

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
			rot.X = (float)(Math.Cos(angle) * point.X - Math.Sin(angle) * point.Y);
			rot.X = (float)(Math.Cos(angle) * point.Y + Math.Sin(angle) * point.X);
			return rot;
		}
	}
}
