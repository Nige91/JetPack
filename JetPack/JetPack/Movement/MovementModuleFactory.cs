using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace JetPack.Movement
{
	static class MovementModuleFactory
	{
		public static MovementModule CreateStandardHorizontalModule(SKPoint coords, SKSize size, SKSize explSize, float speed)
		{
			MovementModule module = CreateEmptyModule(coords, size, explSize);
			module.AddUnit(new MovementModuleUnit(new SKPoint(speed, 0)));
			return module;
		}

		public static MovementModule CreateEmptyModule(SKPoint coords, SKSize size, SKSize explSize)
		{
			MovementModule module = new MovementModule(coords, size, explSize);
			return module;
		}
	}
}
