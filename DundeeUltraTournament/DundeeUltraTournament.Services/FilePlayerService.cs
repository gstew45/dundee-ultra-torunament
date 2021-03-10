using DundeeUltraTournament.Core.Interfaces;
using DundeeUltraTournament.Core.Interfaces.Stores;
using DundeeUltraTournament.Core.Models;
using System.Collections.Generic;

namespace DundeeUltraTournament.Services
{
	public class FilePlayerService : IPlayerService
	{
		private readonly IPlayerStore m_playerStore;

		public FilePlayerService(IPlayerStore playerStore) => m_playerStore = playerStore;

		public IEnumerable<Player> GetRegisteredPlayers()
		{
			return m_playerStore.GetRegisteredPlayers();
		}
	}
}
