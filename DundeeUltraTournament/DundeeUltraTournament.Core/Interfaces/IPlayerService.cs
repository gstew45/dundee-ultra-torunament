using DundeeUltraTournament.Core.Models;
using System.Collections.Generic;

namespace DundeeUltraTournament.Core.Interfaces
{
	public interface IPlayerService
	{
		IEnumerable<Player> GetRegisteredPlayers();
	}
}
