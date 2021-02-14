using SkiaSharp;

namespace JetPack.Movement
{
	class LinearMovUnit : MovementModuleUnit
	{
		private SKPoint distance;

		private LinearMovUnit()
		{
		}

		public LinearMovUnit(SKPoint distance)
		{
			this.distance = distance;
		}

		public override MovementModuleUnit Copy()
		{
			LinearMovUnit unit = new LinearMovUnit();
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
