using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace JetPack.Movement
{
	class MovementModule
	{
		public SKPoint coords { get; private set; }
		public SKSize size { get; private set; }
		public SKSize scale { get; private set; } = new SKSize(1, 1);
		public List<MovementModuleUnit> movementModuleUnits { get; set; }

		public float speed { get; private set; } = 1;

		public MovementModule(SKPoint coords, SKSize size)
		{
			this.coords = coords;
			this.size = size;
		}

		public void AddUnit(MovementModuleUnit unit)
		{
			movementModuleUnits.Add(unit);
		}

		public void Move()
		{
			foreach (var unit in movementModuleUnits)
			{
				coords += unit.Move(speed);
			}
		}

		public void SetSpeed(float speed)
		{
			RecalibrateUnitPhases(this.speed);
			this.speed = speed;
		}

		private void RecalibrateUnitPhases(float speed)
		{
			foreach (var unit in movementModuleUnits)
			{
				unit.RecalibratePhase(speed);
			}
		}
	}

	class MovementModuleUnit
	{
		public SKPoint distance;
		public float ampMin { get; private set; }
		public float ampMax { get; private set; }
		public int phaseDuration { get; private set; }
		public long phaseStartTime { get; private set; }

		public MovementModuleUnit(SKPoint distance)
		{
			this.distance = distance;
			this.phaseDuration = 0;
			this.phaseDuration = 0;
		}

		public MovementModuleUnit(SKPoint distance, float ampMin, float ampMax, int phaseDuration)
		{
			this.distance = distance;
			this.ampMin = ampMin;
			this.ampMax = ampMax;
			this.phaseDuration = phaseDuration;
			this.phaseStartTime = Helper.GetMilliseconds();
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
			return 1000 / Globals.fps;
		}
	}
}
