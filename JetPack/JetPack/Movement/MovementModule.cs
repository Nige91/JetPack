using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;
using SkiaSharp.Views.Forms;

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

		public MovementModule()
		{
			movementModuleUnits = new List<MovementModuleUnit>();
		}

		public MovementModule(SKPoint coords, SKSize size, SKSize explSize)
		{
			this.coords = coords;
			this.size = size;
			this.explSize = explSize;
			movementModuleUnits = new List<MovementModuleUnit>();
		}

		public MovementModule Copy(SKPoint coords)
		{
			MovementModule module = new MovementModule();
			module.coords = new SKPoint(
				this.coords.X + coords.X, 
				this.coords.Y + coords.Y
			);
			module.size = new SKSize(this.size.Width, this.size.Height);
			module.explSize = new SKSize(this.explSize.Width, this.explSize.Height);
			module.movementModuleUnits = new List<MovementModuleUnit>();
			foreach(var unit in this.movementModuleUnits)
			{
				module.movementModuleUnits.Add(unit.Copy());
			}
			module.rotation = this.rotation;
			return module;
		}

		public void AddUnit(MovementModuleUnit unit)
		{
			movementModuleUnits.Add(unit);
		}

		public void Move()
		{
			if(rotation == 0)
			{
				foreach (var unit in movementModuleUnits)
					coords += unit.Move();
			}
			else
			{
				foreach (var unit in movementModuleUnits)
					coords += Helper.Rotate(unit.Move(), rotation);
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
