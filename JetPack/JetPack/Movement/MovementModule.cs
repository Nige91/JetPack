using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace JetPack.Movement
{
	//TODO overthink MovementModule class structure
	[Serializable()]
	class MovementModule
	{
		public SKPoint coords { get; private set; }
		public SKSize size { get; private set; }
		public SKSize scale { get; private set; } = new SKSize(1, 1);
		public List<MovementModuleUnit> movementModuleUnits { get; set; }

		public float speed { get; private set; } = 1;

		public MovementModule()
		{
			movementModuleUnits = new List<MovementModuleUnit>();
		}

		public MovementModule(SKPoint coords, SKSize size)
		{
			this.coords = coords;
			this.size = size;
			movementModuleUnits = new List<MovementModuleUnit>();
		}

		public MovementModule Copy(SKPoint coords)
		{
			MovementModule module = new MovementModule();
			module.coords = new SKPoint(coords.X, coords.Y);
			module.size = new SKSize(this.size.Width, this.size.Height);
			module.scale = new SKSize(this.scale.Width, this.scale.Height);
			module.movementModuleUnits = new List<MovementModuleUnit>();
			foreach(var unit in this.movementModuleUnits)
			{
				module.movementModuleUnits.Add(unit.Copy());
			}
			module.speed = this.speed;
			return module;
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

		public SKRect GetRect()
		{
			SKRect rect = new SKRect(coords.X, coords.Y, coords.X + size.Width, coords.Y + size.Height);
			return rect;
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
			return (float)1000 / ((float)Globals.fps * (float)Globals.normalTimeUnitInMs);
		}
	}
}
