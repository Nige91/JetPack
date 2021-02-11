using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;

namespace JetPack.Movement
{
	abstract class MovementModuleUnit
	{
		protected long lastStepTime = 0;

		public abstract MovementModuleUnit Copy();

		public abstract SKPoint Move();

		protected float GetLoopTime()
		{
			if (lastStepTime == 0)
			{
				lastStepTime = Helper.GetMilliseconds() - (1000 / Settings.General.fps);
			}
			float loopTimeMs = Helper.GetMilliseconds() - lastStepTime;
			lastStepTime = Helper.GetMilliseconds();
			return loopTimeMs / Settings.General.normalTimeUnitInMs;
		}
	}
}
