using DundeeUltraTournament.Core.Models;
using System.Collections.Generic;

namespace DundeeUltraTournament.Persistence.Parsers
{
	public interface IPlayersParser
	{
		IEnumerable<Player> GetPlayers(string input);
	}
}
