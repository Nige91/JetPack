using SkiaSharp;
using System.Collections.Generic;

namespace JetPack.Movement
{
	//TODO overthink MovementModule class structure
	class MovementModule
	{
		public float rotation { get; set; } = 0;
		public SKPoint coords { get; set; }
		public SKSize size { get; private set; }
		public SKSize explSize { get; private set; }
		public List<MovementModuleUnit> movementModuleUnits { get; private set; }
		public float xCoordLimit { get; private set; } = 0;

		public MovementModule()
		{
			movementModuleUnits = new List<MovementModuleUnit>();
		}

		public MovementModule(SKPoint coords, SKSize size, SKSize explSize) : this()
		{
			this.coords = coords;
			this.size = size;
			this.explSize = explSize;
		}

		public MovementModule(
			SKPoint coords, 
			SKSize size, 
			SKSize explSize, 
			float xCoordLimit
		) : this(coords, size, explSize)
		{
			this.xCoordLimit = xCoordLimit;
		}

		public MovementModule Copy(SKPoint coords)
		{
			MovementModule module = new MovementModule();
			module.coords = new SKPoint(
				this.coords.X + coords.X,
				this.coords.Y + coords.Y
			);
			module.size = new SKSize(size.Width, size.Height);
			module.explSize = new SKSize(explSize.Width, explSize.Height);
			module.movementModuleUnits = new List<MovementModuleUnit>();
			foreach (var unit in movementModuleUnits)
			{
				module.movementModuleUnits.Add(unit.Copy());
			}
			module.rotation = rotation;
			return module;
		}

		public void AddUnit(MovementModuleUnit unit)
		{
			movementModuleUnits.Add(unit);
		}

		public void Move()
		{
			if (rotation == 0)
			{
				foreach (var unit in movementModuleUnits)
					coords += unit.Move();
			}
			else
			{
				foreach (var unit in movementModuleUnits)
					coords += Helper.Rotate(unit.Move(), rotation);
			}
			if(xCoordLimit != 0 && coords.X <= xCoordLimit)
			{
				coords = new SKPoint(xCoordLimit, coords.Y);
			}
		}

		public SKRect GetRect()
		{
			SKRect rect = new SKRect(
				coords.X,
				coords.Y,
				coords.X + size.Width,
				coords.Y + size.Height
			);
			return rect;
		}

		public SKRect GetRectExpl()
		{
			SKRect rect = new SKRect(
				coords.X,
				coords.Y,
				coords.X + explSize.Width,
				coords.Y + explSize.Height
			);
			return rect;
		}
	}
}
