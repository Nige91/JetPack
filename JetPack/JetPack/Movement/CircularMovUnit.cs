using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;
using JetPack.Timing;

namespace JetPack.Movement
{
	class CircularMovUnit : MovementModuleUnit
	{
		private float radius;
		private float cycleDuration;
		private long cycleStart;
		private float prevAngle = 0;

		private LoopTimer loopTimer;

		public CircularMovUnit(float radius, float cycleDuration) : this()
		{
			this.radius = radius;
			this.cycleDuration = cycleDuration;
		}

		private CircularMovUnit()
		{
			loopTimer = LoopTimer.GetInstance();
			cycleStart = loopTimer.GetTotalMs();
		}

		public override MovementModuleUnit Copy()
		{
			CircularMovUnit unit = new CircularMovUnit();
			unit.radius = radius;
			unit.cycleDuration = cycleDuration;
			return unit;
		}

		public override SKPoint Move()
		{
			float angle = GetAngle();
			SKPoint r1 = new SKPoint(
				radius * (float)Math.Cos(prevAngle),
				radius * (float)Math.Sin(prevAngle)
			);
			SKPoint r2 = new SKPoint(
				radius * (float)Math.Cos(angle),
				radius * (float)Math.Sin(angle)
			);
			prevAngle = angle;
			return r2 - r1;
		}

		private float GetAngle()
		{
			float elapsedSec = ((float)(loopTimer.GetTotalMs() - cycleStart))/1000;
			float angle = 2*(float)Math.PI*(elapsedSec % cycleDuration) / cycleDuration;
			return angle;
		}
	}
}
