using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace JetPack.Movement
{
	class MovementModuleUnit
	{
		public SKPoint distance;
		public float ampMin { get; private set; }
		public float ampMax { get; private set; }
		public int phaseDuration { get; private set; }
		public long phaseStartTime { get; private set; }

		public MovementModuleUnit()
		{
		}

		public MovementModuleUnit(SKPoint distance)
		{
			this.distance = distance;
			this.phaseDuration = 0;
			this.phaseStartTime = 0;
		}

		public MovementModuleUnit(SKPoint distance, float ampMin, float ampMax, int phaseDuration)
		{
			this.distance = distance;
			this.ampMin = ampMin;
			this.ampMax = ampMax;
			this.phaseDuration = phaseDuration;
			this.phaseStartTime = Helper.GetMilliseconds();
		}

		public MovementModuleUnit Copy()
		{
			MovementModuleUnit unit = new MovementModuleUnit();
			unit.distance = new SKPoint(this.distance.X, this.distance.Y);
			unit.ampMin = this.ampMin;
			unit.ampMax = this.ampMax;
			unit.phaseDuration = this.phaseDuration;
			unit.phaseStartTime = this.phaseStartTime;
			return unit;
		}

		public SKPoint Move(float speed)
		{
			SKPoint finalDistance = new SKPoint();
			finalDistance.X = distance.X * GetAmplitude() * GetLoopTime();
			finalDistance.Y = distance.Y * GetAmplitude() * GetLoopTime();
			return finalDistance;
		}

		//TODO RecalibratePhaseStart
		public void RecalibratePhase(float speed)
		{

		}

		//TODO implement GetAmplitude
		private float GetAmplitude()
		{
			return 1;
		}

		//TODO implement GetLoopTime
		private float GetLoopTime()
		{
			return (float)1000 / ((float)Settings.General.fps * (float)Settings.General.normalTimeUnitInMs);
		}
	}
}
