using DundeeUltraTournament.Services;
using System;

namespace DundeeUltraTournament.DataPoller
{
	class Program
	{
		static void Main(string[] args)
		{
			PollData poller = new PollData(new SbmmDataGenerator());
			poller.Poll();
		}
	}
}
