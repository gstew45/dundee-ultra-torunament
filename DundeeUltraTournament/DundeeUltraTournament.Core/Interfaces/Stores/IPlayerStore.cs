using DundeeUltraTournament.Core.Models;
using System.Collections.Generic;

namespace DundeeUltraTournament.Core.Interfaces.Stores
{
	public interface IPlayerStore
	{
		IEnumerable<Player> GetRegisteredPlayers();
	}
}
