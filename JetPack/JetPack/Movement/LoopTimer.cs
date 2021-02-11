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
			return 1 / loopTime;
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
		}
	}
}
