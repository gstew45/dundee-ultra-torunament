using DundeeUltraTournament.Core.Interfaces;
using DundeeUltraTournament.Core.Models;
using System.Collections.Generic;

namespace DundeeUltraTournament.Services
{
	public class WarzoneDataGenerator : IDataGenerator
	{
		private readonly IPlayerService m_playerService;
		private readonly IWarzoneService m_warzoneService;
		private readonly ITeamResultService m_teamResultService;

		public WarzoneDataGenerator(IPlayerService playerService, IWarzoneService warzoneService, ITeamResultService teamResultService)
		{
			m_playerService = playerService;
			m_warzoneService = warzoneService;
			m_teamResultService = teamResultService;
		}

		public void GenerateData()
		{
			IEnumerable<Player> registeredPlayers = m_playerService.GetRegisteredPlayers();

			foreach (Player player in registeredPlayers)
			{
				PlayerMatchInfo playerMatchInfo = m_warzoneService.GetPlayerMatchInfo(player.Username, player.Platform);

				TeamResult teamResult = m_warzoneService.GetTeamResult(playerMatchInfo.MatchId, playerMatchInfo.Placement);
				m_teamResultService.StoreTeamResult(teamResult);
			}
		}
	}
}
