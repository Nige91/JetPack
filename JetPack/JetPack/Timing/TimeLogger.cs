using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using JetPack.Movement;

namespace JetPack.Timing
{
	sealed class TimeLogger
	{
		private static readonly TimeLogger instance = new TimeLogger();

		private Dictionary<string, long> startTimes;
		private Dictionary<string, List<long>> loggedTimesLists;
		private Dictionary<string, float> meanTimes;
		private Dictionary<string, float> maxTimes;
		private Stopwatch watch;

		private readonly bool active = true;


		static TimeLogger()
		{

		}

		private TimeLogger()
		{
			if (active)
			{
				startTimes = new Dictionary<string, long>();
				loggedTimesLists = new Dictionary<string, List<long>>();
				meanTimes = new Dictionary<string, float>();
				maxTimes = new Dictionary<string, float>();
				watch = new Stopwatch();
				watch.Start(); 
			}
		}

		public static TimeLogger GetInstance()
		{
			return instance;
		}


		public void StartLog(string identifier)
		{
			if (active)
			{
				if (!startTimes.ContainsKey(identifier))
				{
					startTimes.Add(identifier, watch.ElapsedMilliseconds);
				}
				else
				{
					startTimes[identifier] = watch.ElapsedMilliseconds;
				}
			}
		}

		public void FinishLog(string identifier)
		{
			if (active)
			{
				Debug.Assert(startTimes.ContainsKey(identifier));
				long loggedTime = watch.ElapsedMilliseconds - startTimes[identifier];
				if (!loggedTimesLists.ContainsKey(identifier))
				{
					loggedTimesLists.Add(identifier, new List<long>());
				}
				loggedTimesLists[identifier].Add(loggedTime); 
			}
		}

		public void CalculateMeansAndMax()
		{
			if (active)
			{
				foreach (var key in loggedTimesLists.Keys)
				{
					if (!meanTimes.ContainsKey(key))
					{
						meanTimes.Add(key, 0);
					}
					if (!maxTimes.ContainsKey(key))
					{
						maxTimes.Add(key, 0);
					}
					long total = 0;
					long max = 0;
					foreach (var time in loggedTimesLists[key])
					{
						total += time;
						if (time > max)
							max = time;
					}
					float mean = (float)total / loggedTimesLists[key].Count;
					meanTimes[key] = mean;
					maxTimes[key] = max;
				} 
			}
		}
	}
}
