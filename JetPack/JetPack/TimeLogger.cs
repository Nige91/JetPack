using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace JetPack
{
	sealed class TimeLogger
	{
		private static readonly TimeLogger instance = new TimeLogger();

		private Dictionary<string, long> startTimes;
		private Dictionary<string, List<long>> loggedTimesLists;
		private Dictionary<string, float> meanTimes;


		static TimeLogger()
		{

		}

		private TimeLogger()
		{
			startTimes = new Dictionary<string, long>();
			loggedTimesLists = new Dictionary<string, List<long>>();
			meanTimes = new Dictionary<string, float>();
		}

		public static TimeLogger GetInstance()
		{
			return instance;
		}


		public void StartLog(string identifier)
		{
			if (!startTimes.ContainsKey(identifier))
			{
				startTimes.Add(identifier, Helper.GetMilliseconds());
			}
			else
			{
				startTimes[identifier] = Helper.GetMilliseconds();
			}
		}

		public void FinishLog(string identifier)
		{
			Debug.Assert(startTimes.ContainsKey(identifier));
			long loggedTime = Helper.GetMilliseconds() - startTimes[identifier];
			if (!loggedTimesLists.ContainsKey(identifier))
			{
				loggedTimesLists.Add(identifier, new List<long>());
			}
			loggedTimesLists[identifier].Add(loggedTime);
		}

		public void CalculateMeans()
		{
			foreach(var key in loggedTimesLists.Keys)
			{
				if (!meanTimes.ContainsKey(key))
				{
					meanTimes.Add(key, 0);
				}
				long total = 0;
				foreach(var time in loggedTimesLists[key])
				{
					total += time;
				}
				float mean = (float)total / loggedTimesLists[key].Count;
				meanTimes[key] = mean;
			}
		}
	}
}
