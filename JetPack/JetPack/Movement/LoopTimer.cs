using System;
using System.Collections.Generic;
using System.Text;

namespace JetPack.Movement
{
	sealed class LoopTimer
	{
		private static readonly LoopTimer instance =
			new LoopTimer();

		private long lastStepTime = 0;
		private float loopTime = 1f/ Settings.General.fps;
		private float[] loopTimeArray = 
			new float[Settings.General.loopTimeArrayLength];
		private int loopTimeArrayIndex = 0;


		static LoopTimer()
		{

		}

		private LoopTimer()
		{

		}

		public static LoopTimer GetInstance()
		{
			return instance;
		}

		public float GetLoopTime()
		{
			return loopTime;
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
			if (lastStepTime == 0)
			{
				lastStepTime = Helper.GetMilliseconds() - (1000 / Settings.General.fps);
			}
			float loopTimeMs = Helper.GetMilliseconds() - lastStepTime;
			lastStepTime = Helper.GetMilliseconds();
			loopTime =  loopTimeMs / Settings.General.normalTimeUnitInMs;
			loopTimeArray[loopTimeArrayIndex%30] = loopTime;
			loopTimeArrayIndex++;
		}
	}
}
