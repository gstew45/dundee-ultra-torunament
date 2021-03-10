using DundeeUltraTournament.Core.Models;
using System.Collections.Generic;
using System.Text.Json;

namespace DundeeUltraTournament.Persistence.Parsers
{
	public class JsonPlayersParser : IPlayersParser
	{
		public IEnumerable<Player> GetPlayers(string input)
		{
			return JsonSerializer.Deserialize<IEnumerable<Player>>(input);
		}
	}
}
