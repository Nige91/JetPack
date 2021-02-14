using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;
using JetPack.Timing;

namespace JetPack.Movement
{
	class BackAndForthMovUnit : MovementModuleUnit
	{
		private SKPoint distance;
		private float cycleDuration;
		private long cycleStart;

		private LoopTimer loopTimer;

		public BackAndForthMovUnit(SKPoint distance, float cycleDuration) : this()
		{
			this.distance = distance;
			this.cycleDuration = cycleDuration;
		}

		private BackAndForthMovUnit()
		{
			loopTimer = LoopTimer.GetInstance();
			cycleStart = loopTimer.GetTotalMs();
		}

		public override MovementModuleUnit Copy()
		{
			BackAndForthMovUnit unit = new BackAndForthMovUnit();
			unit.distance = new SKPoint(distance.X, distance.Y);
			unit.cycleDuration = cycleDuration;
			return unit;
		}

		public override SKPoint Move()
		{
			SKPoint finalDistance = new SKPoint();
			if (IsGoingForward())
			{
				finalDistance.X = distance.X * GetLoopTime();
				finalDistance.Y = distance.Y * GetLoopTime();
			}
			else
			{
				finalDistance.X = - distance.X * GetLoopTime();
				finalDistance.Y = - distance.Y * GetLoopTime();
			}
			return finalDistance;
		}

		private bool IsGoingForward()
		{
			float elapsedSec = ((float)(loopTimer.GetTotalMs() - cycleStart)) / 1000;
			return (elapsedSec % cycleDuration) / cycleDuration < 0.5;
		}
	}
}
