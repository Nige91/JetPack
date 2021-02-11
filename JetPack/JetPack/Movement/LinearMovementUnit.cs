using SkiaSharp;

namespace JetPack.Movement
{
	class LinearMovementUnit : MovementModuleUnit
	{
		public SKPoint distance { get; set; }

		public LinearMovementUnit()
		{
		}

		public LinearMovementUnit(SKPoint distance)
		{
			this.distance = distance;
		}

		public override MovementModuleUnit Copy()
		{
			LinearMovementUnit unit = new LinearMovementUnit();
			unit.distance = new SKPoint(distance.X, distance.Y);
			return unit;
		}

		public override SKPoint Move()
		{
			SKPoint finalDistance = new SKPoint();
			finalDistance.X = distance.X * GetLoopTime();
			finalDistance.Y = distance.Y * GetLoopTime();
			return finalDistance;
		}
	}
}
