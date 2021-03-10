using DundeeUltraTournament.Core.Interfaces;
using DundeeUltraTournament.Core.Interfaces.Stores;
using DundeeUltraTournament.Persistence;
using DundeeUltraTournament.Persistence.Parsers;
using DundeeUltraTournament.Services;
using System.IO.Abstractions;

namespace DundeeUltraTournament.DataPoller
{
	class Program
	{
		static void Main(string[] args)
		{
			IPlayerStore playerStore = new FilePlayerStore(new FileSystem(), new JsonPlayersParser());
			IPlayerService playerService = new FilePlayerService(playerStore);
			IDataGenerator dataGenerator = new WarzoneDataGenerator(playerService, null, null);

			PollData poller = new PollData(dataGenerator);
			poller.Poll();
		}
	}
}