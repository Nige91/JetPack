using SkiaSharp;
using System;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using Plugin.SimpleAudioPlayer;
using JetPack.Timing;

namespace JetPack
{
	static class Helper
	{
		static Random rand = new Random();
		static TimeLogger timeLogger = TimeLogger.GetInstance();

		static Helper()
		{
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

		public static ISimpleAudioPlayer LoadAudioPlayer(string resourceId)
		{
			Assembly assembly = typeof(App).GetTypeInfo().Assembly;
			var stream= assembly.GetManifestResourceStream(resourceId);
			var player = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
			player.Load(stream);
			return player;
		}

		public static void StartTimeLog(string identifier)
		{
			timeLogger.StartLog(identifier);
		}

		public static void FinishTimeLog(string identifier)
		{
			timeLogger.FinishLog(identifier);
		}
	}
}
