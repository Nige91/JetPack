using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace JetPack.Movement
{
	class MovementModuleUnit
	{
		public SKPoint distance { get; set; }
		private long lastStepTime;

		public MovementModuleUnit()
		{
			this.lastStepTime = 0;
		}

		public MovementModuleUnit(SKPoint distance)
		{
			this.distance = distance;
			this.lastStepTime = 0;
		}

		public MovementModuleUnit Copy()
		{
			MovementModuleUnit unit = new MovementModuleUnit();
			unit.distance = new SKPoint(this.distance.X, this.distance.Y);
			return unit;
		}

		public SKPoint Move()
		{
			SKPoint finalDistance = new SKPoint();
			finalDistance.X = distance.X * GetLoopTime();
			finalDistance.Y = distance.Y * GetLoopTime();
			return finalDistance;
		}

		private float GetLoopTime()
		{
			if(lastStepTime == 0)
			{
				lastStepTime = Helper.GetMilliseconds() - (1000/Settings.General.fps);
			}
			float loopTimeMs = Helper.GetMilliseconds() - lastStepTime;
			lastStepTime = Helper.GetMilliseconds();
			return loopTimeMs / Settings.General.normalTimeUnitInMs;
		}
	}
}
