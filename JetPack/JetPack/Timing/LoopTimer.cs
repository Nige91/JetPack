using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

//TODO move out of movement namespace
namespace JetPack.Timing
{
	sealed class LoopTimer
	{
		private static readonly LoopTimer instance =
			new LoopTimer();

		private long lastStepTime = 0;
		private long totalTime = 0;
		private float loopTime = 1f/ Settings.General.fps;
		private float[] loopTimeArray = 
			new float[Settings.General.loopTimeArrayLength];
		private int loopTimeArrayIndex = 0;
		private Stopwatch watch = new Stopwatch();


		static LoopTimer()
		{

		}

		private LoopTimer()
		{
			watch.Start();
		}

		public static LoopTimer GetInstance()
		{
			return instance;
		}

		public float GetLoopTime()
		{
			return loopTime;
		}

		public long GetTotalMs()
		{
			return totalTime;
		}

		public float GetFPS()
		{
			float loopTimeSum = 0;
			foreach (var x in loopTimeArray)
				loopTimeSum += x;
			float loopTimeMean = loopTimeSum / Settings.General.loopTimeArrayLength;
			return 1 / loopTimeMean;
		}

		public void MeasureTime()
		{
			totalTime = watch.ElapsedMilliseconds;
			if (lastStepTime == 0)
			{
				lastStepTime = GetTotalMs() - (1000 / Settings.General.fps);
			}
			float loopTimeMs = GetTotalMs() - lastStepTime;
			lastStepTime = GetTotalMs();
			loopTime =  loopTimeMs / Settings.General.normalTimeUnitInMs;
			loopTimeArray[loopTimeArrayIndex%30] = loopTime;
			loopTimeArrayIndex++;
		}
	}
}
