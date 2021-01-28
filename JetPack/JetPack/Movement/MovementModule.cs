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
		public SKPoint coords { get; private set; }
		public SKSize size { get; private set; }
		public SKSize explSize { get; private set; }
		public SKSize scale { get; private set; } = new SKSize(1, 1);
		public List<MovementModuleUnit> movementModuleUnits { get; set; }

		public float speed { get; private set; } = 1;

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
}
