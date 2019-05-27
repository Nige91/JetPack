using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using SkiaSharp;
using SkiaSharp.Views.Forms;

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
	}
}
