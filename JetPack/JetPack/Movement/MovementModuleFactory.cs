using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace JetPack.Movement
{
	static class MovementModuleFactory
	{
		public static MovementModule CreateStandardHorizontalModule(SKPoint coords, SKSize size, float speed)
		{
			MovementModule module = CreateEmptyModule(coords, size);
			module.AddUnit(new MovementModuleUnit(new SKPoint(speed, 0)));
			return module;
		}

		public static MovementModule CreateEmptyModule(SKPoint coords, SKSize size)
		{
			MovementModule module = new MovementModule(coords, size);
			return module;
		}
	}
}
