using DundeeUltraTournament.Core.Interfaces;
using DundeeUltraTournament.Core.Models;
using DundeeUltraTournament.Services;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace DundeeUltraTournament.Tests.Services
{
	[TestClass]
	public class WarzoneDataGeneratorTests
	{
		private WarzoneDataGenerator m_testSubject;
		private IPlayerService m_playerService;
		private IWarzoneService m_warzoneService;
		private ITeamResultService m_teamResultService;

		private IEnumerable<Player> m_players;

		[TestInitialize]
		public void Init()
		{
			m_players = GetMockPlayers();

			m_playerService = A.Fake<IPlayerService>();
			A.CallTo(() => m_playerService.GetRegisteredPlayers()).Returns(m_players);

			m_warzoneService = A.Fake<IWarzoneService>();
			m_teamResultService = A.Fake<ITeamResultService>();
			m_testSubject = new WarzoneDataGenerator(m_playerService, m_warzoneService, m_teamResultService);
		}

		[TestMethod]
		public void SbmmDataGenerator_Can_Be_Constructed()
		{
			Assert.IsNotNull(m_testSubject);
		}

		[TestMethod]
		public void SbmmDataGenerator_Implements_IDataGenerator()
		{
			Assert.IsInstanceOfType(m_testSubject, typeof(IDataGenerator));
		}

		[TestMethod]
		public void GenerateData_GetsRegisteredPlayers()
		{
			m_testSubject.GenerateData();

			A.CallTo(() => m_playerService.GetRegisteredPlayers()).MustHaveHappenedOnceExactly();
		}

		[TestMethod]
		public void GenerateData_GetsMatchInfo_Foreach_Player()
		{
			m_testSubject.GenerateData();

			foreach (Player player in m_players)
			{
				A.CallTo(() => m_warzoneService.GetPlayerMatchInfo(player.Username, player.Platform)).MustHaveHappenedOnceExactly();
			}
		}

		[TestMethod]
		public void GenerateData_GetsTeamResult_For_Match()
		{
			foreach (Player player in m_players)
			{
				PlayerMatchInfo playerMatchInfo = new PlayerMatchInfo() { MatchId = $"TestMatchId-{player.Username}", PlayerUsername = player.Username, Placement = 2, StartedAt = "TestStartedAtTime" };
				A.CallTo(() => m_warzoneService.GetPlayerMatchInfo(player.Username, player.Platform)).Returns(playerMatchInfo);
			}

			m_testSubject.GenerateData();

			foreach (Player player in m_players)
			{
				PlayerMatchInfo playerMatchInfo = new PlayerMatchInfo() { MatchId = $"TestMatchId-{player.Username}", PlayerUsername = player.Username, Placement = 2, StartedAt = "TestStartedAtTime" };
				
				A.CallTo(() => m_warzoneService.GetTeamResult(playerMatchInfo.MatchId, playerMatchInfo.Placement)).MustHaveHappenedOnceExactly();
			}
		}

		[TestMethod]
		public void GenerateData_StoresTeamResult()
		{
			foreach (Player player in m_players)
			{
				PlayerMatchInfo playerMatchInfo = new PlayerMatchInfo() { MatchId = $"TestMatchId-{player.Username}", PlayerUsername = player.Username, Placement = 2, StartedAt = "TestStartedAtTime" };
				A.CallTo(() => m_warzoneService.GetPlayerMatchInfo(player.Username, player.Platform)).Returns(playerMatchInfo);

				TeamResult teamResult = new TeamResult() { Captain = player.Username, Placement = playerMatchInfo.Placement, StartedAt = playerMatchInfo.StartedAt };
				A.CallTo(() => m_warzoneService.GetTeamResult(playerMatchInfo.MatchId, playerMatchInfo.Placement)).Returns(teamResult);
			}

			m_testSubject.GenerateData();

			foreach (Player player in m_players)
			{
				PlayerMatchInfo playerMatchInfo = new PlayerMatchInfo() { MatchId = $"TestMatchId-{player.Username}", PlayerUsername = player.Username, Placement = 2, StartedAt = "TestStartedAtTime" };
				TeamResult teamResult = new TeamResult() { Captain = player.Username, Placement = playerMatchInfo.Placement, StartedAt = playerMatchInfo.StartedAt };

				A.CallTo(() => m_teamResultService.StoreTeamResult(A<TeamResult>.That.Matches(tr => tr.Captain.Equals(teamResult.Captain)))).MustHaveHappenedOnceExactly();
			}
		}

		private IEnumerable<Player> GetMockPlayers()
		{
			List<Player> players = new List<Player>()
			{
				CreatePlayer("TestUser1", "xbl"),
				CreatePlayer("TestUser2", "psn")
			};

			return players;
		}

		private Player CreatePlayer(string username, string platform)
		{
			return new Player() { Username = username, Platform = platform };
		}

	}
}
