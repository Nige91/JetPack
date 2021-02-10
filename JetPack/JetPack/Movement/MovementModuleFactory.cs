using SkiaSharp;

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

		public MovementModule CreateStandardHorizontalModule(
			SKPoint coords,
			SKSize size,
			SKSize explSize,
			float speed,
			float xCoordLimit = 0
		)
		{
			MovementModule module = CreateEmptyModule(coords, size, explSize, xCoordLimit);
			module.AddUnit(new MovementModuleUnit(new SKPoint(speed, 0)));
			return module;
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
	}
}
