using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;

namespace JetPack.Movement
{
	abstract class MovementModuleUnit
	{
		protected LoopTimer loopTimer = LoopTimer.GetInstance();

		public abstract MovementModuleUnit Copy();

		public abstract SKPoint Move();

		protected float GetLoopTime()
		{
			return loopTimer.GetLoopTime();
		}
	}
}
