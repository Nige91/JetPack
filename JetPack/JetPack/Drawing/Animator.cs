using SkiaSharp;

namespace JetPack.Drawing
{
	class Animator
	{
		private int nStepsPre;
		private int nStepsHold;
		private int nStepsPost;
		private int stepDuration;
		private long animationStartTime;
		private long animationStopTime;
		private SKBitmap[] bitmapsArrayPre;
		private SKBitmap[] bitmapsArrayHold;
		private SKBitmap[] bitmapsArrayPost;
		private bool on = false;

		public Animator(string resourceString, int nSteps, int stepDuration)
		{
			nStepsPre = 0;
			nStepsHold = nSteps;
			nStepsPost = 0;
			this.stepDuration = stepDuration;
			bitmapsArrayHold = new SKBitmap[nSteps];
			for (int i = 0; i < nSteps; i++)
			{
				bitmapsArrayHold[i] = Helper.LoadBitmap(
					resourceString + GetStepIdentifierString(i) + ".png"
				);
			}
			//TODO move call out of constructor
			Start();
		}

		public Animator(
			string resourceStringPre,
			string resourceStringHold,
			string resourceStringPost,
			int nStepsPre,
			int nStepsHold,
			int nStepsPost,
			int stepDuration
		)
		{
			this.nStepsPre = nStepsPre;
			this.nStepsHold = nStepsHold;
			this.nStepsPost = nStepsPost;
			this.stepDuration = stepDuration;
			bitmapsArrayPre = new SKBitmap[nStepsPre];
			bitmapsArrayHold = new SKBitmap[nStepsHold];
			bitmapsArrayPost = new SKBitmap[nStepsPost];
			for (int i = 0; i < nStepsPre; i++)
			{
				bitmapsArrayPre[i] = Helper.LoadBitmap(
					resourceStringPre + GetStepIdentifierString(i) + ".png"
				);
			}
			for (int i = 0; i < nStepsHold; i++)
			{
				bitmapsArrayHold[i] = Helper.LoadBitmap(
					resourceStringHold + GetStepIdentifierString(i) + ".png"
				);
			}
			for (int i = 0; i < nStepsPost; i++)
			{
				bitmapsArrayPost[i] = Helper.LoadBitmap(
					resourceStringPost + GetStepIdentifierString(i) + ".png"
				);
			}
		}

		public void Start()
		{
			on = true;
			animationStartTime = Helper.GetMilliseconds();
		}

		public void Stop()
		{
			on = false;
			animationStopTime = Helper.GetMilliseconds();
		}

		public void Draw(SKCanvas canvas, SKRect rect)
		{
			long time = Helper.GetMilliseconds();
			if (NeedsToDraw(time))
			{
				SKBitmap[] ba = GetBitmapArray(time);
				canvas.DrawBitmap(ba[GetAnimationStep(time)], rect);
			}

		}

		private int GetAnimationStep(long time)
		{
			if (on)
			{
				long msPassedTotal = time - animationStartTime;
				int stepsTotal = (int)msPassedTotal / stepDuration;
				if (stepsTotal < nStepsPre)
				{
					return stepsTotal;
				}
				else
				{
					return (stepsTotal - nStepsPre) % nStepsHold;
				}
			}
			else
			{
				long msPassedTotal = time - animationStopTime;
				int stepsTotal = (int)msPassedTotal / stepDuration;
				return stepsTotal;
			}
		}

		private SKBitmap[] GetBitmapArray(long time)
		{
			if (on)
			{
				long msPassedTotal = time - animationStartTime;
				if (msPassedTotal / stepDuration < nStepsPre)
				{
					return bitmapsArrayPre;
				}
				else
				{
					return bitmapsArrayHold;
				}
			}
			else
			{
				return bitmapsArrayPost;
			}
		}

		private bool NeedsToDraw(long time)
		{
			if (on)
			{
				return true;
			}
			else
			{
				long msPassedTotal = time - animationStopTime;
				return msPassedTotal / stepDuration < nStepsPost;
			}
		}

		private string GetStepIdentifierString(int n)
		{
			if (n > 9)
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
