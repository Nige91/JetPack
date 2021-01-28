using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace JetPack.Drawing
{
	class Animator
	{
		private string resourceString;
		private int nSteps;
		private int stepDuration;
		private long animationStart;
		private int animationDuration;
		private SKBitmap[] bitmapsArray;

		public Animator(string resourceString, int nSteps, int stepDuration)
		{
			this.resourceString = resourceString;
			this.nSteps = nSteps;
			this.stepDuration = stepDuration;
			this.animationStart = Helper.GetMilliseconds();
			this.animationDuration = stepDuration * nSteps;
			this.bitmapsArray = new SKBitmap[nSteps];
			for(int i = 0; i<nSteps; i++)
			{
				bitmapsArray[i] = Helper.LoadBitmap(
					resourceString + GetStepIdentifierString(i) + ".png"
				);
			}
		}

		public void Draw(SKCanvas canvas, SKRect rect)
		{
			canvas.DrawBitmap(bitmapsArray[GetAnimationStep()], rect);
		}

		private int GetAnimationStep()
		{
			long msPassedTotal = Helper.GetMilliseconds() - animationStart;
			int msPassedCycle = (int)(msPassedTotal % animationDuration);
			return msPassedCycle / stepDuration;
		}

		private string GetStepIdentifierString(int n)
		{
			if(n > 9)
			{
				return n.ToString();
			}
			else
			{
				return "0" + n.ToString();
			}
		}

	}
}
