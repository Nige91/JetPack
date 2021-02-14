using SkiaSharp;
using System;

namespace JetPack.Movement
{
	sealed class MovementModuleFactory
	{
		private static readonly MovementModuleFactory instance =
			new MovementModuleFactory();

		static MovementModuleFactory()
		{

		}

		private MovementModuleFactory()
		{

		}

		public static MovementModuleFactory GetInstance()
		{
			return instance;
		}

		public MovementModule CreateEmptyModule(
			SKPoint coords,
			SKSize size,
			SKSize explSize,
			float xCoordLimit = 0
		)
		{
			MovementModule module = new MovementModule(coords, size, explSize, xCoordLimit);
			return module;
		}

		public MovementModule CreateHorizontalModule(
			SKPoint coords,
			SKSize size,
			SKSize explSize,
			float speed,
			float xCoordLimit = 0
		)
		{
			MovementModule module = CreateEmptyModule(coords, size, explSize, xCoordLimit);
			module.AddUnit(new LinearMovUnit(new SKPoint(speed, 0)));
			return module;
		}

		public MovementModule CreateZigZagModule(
			SKPoint coords,
			SKSize size,
			SKSize explSize,
			float xSpeed,
			float ySpeed,
			float yRange,
			float xCoordLimit = 0
		)
		{
			MovementModule module = CreateEmptyModule(coords, size, explSize, xCoordLimit);
			module.AddUnit(new LinearMovUnit(new SKPoint(xSpeed, 0)));
			float cycleDuration = Math.Abs(yRange / ySpeed);
			module.AddUnit(new BackAndForthMovUnit(new SKPoint(0, ySpeed), cycleDuration));
			return module;
		}

		public MovementModule CreateCircularModule(
			SKPoint coords,
			SKSize size,
			SKSize explSize,
			float xSpeed,
			float radius,
			float cycleDuration,
			float xCoordLimit = 0
		)
		{
			MovementModule module = CreateEmptyModule(coords, size, explSize, xCoordLimit);
			module.AddUnit(new LinearMovUnit(new SKPoint(xSpeed, 0)));
			module.AddUnit(new CircularMovUnit(radius, cycleDuration));
			return module;
		}
	}
}
