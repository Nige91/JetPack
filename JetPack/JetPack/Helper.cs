using System;
using System.Collections.Generic;
using System.Text;

namespace JetPack
{
	static class Helper
	{
		public static long GetMilliseconds()
		{
			return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
		}
	}
}
