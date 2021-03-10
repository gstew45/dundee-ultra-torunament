using DundeeUltraTournament.Core.Models;
using DundeeUltraTournament.Persistence.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace DundeeUltraTournament.Tests.Persistence
{
	[TestClass]
	public class JsonPlayersParserTests
	{
		private JsonPlayersParser m_testSubject;

		[TestInitialize]
		public void Init()
		{
			m_testSubject = new JsonPlayersParser();
		}

		[TestMethod]
		public void JsonPlayersParser_Can_Be_Constructed()
		{
			Assert.IsNotNull(m_testSubject);
		}

		[TestMethod]
		public void JsonPlayersParser_Implements_IPlayersParser()
		{
			Assert.IsInstanceOfType(m_testSubject, typeof(IPlayersParser));
		}

		[TestMethod]
		public void GetPlayers_ReturnsPlayerList()
		{
			string inputJson = "[{\"Username\":\"ryanhendry21\",\"Platform\":\"xbl\"},{\"Username\":\"gstew\",\"Platform\":\"xbl\"}]";

			IEnumerable<Player> players = m_testSubject.GetPlayers(inputJson);

			Assert.AreEqual(2, players.Count());

			Player ryan = players.FirstOrDefault(p => p.Username.Equals("ryanhendry21"));
			Player greg = players.FirstOrDefault(p => p.Username.Equals("gstew"));

			AssertPlayer(players, "ryanhendry21", "xbl");
			AssertPlayer(players, "gstew", "xbl");
		}

		private Player CreatePlayer(string username, string platform)
		{
			return new Player() { Username = username, Platform = platform };
		}

		private void AssertPlayer(IEnumerable<Player> players, string expectedName, string expectedPlatform)
		{
			Player player = players.FirstOrDefault(p => p.Username.Equals(expectedName));

			Assert.IsNotNull(player);
			Assert.AreEqual(expectedName, player.Username);
			Assert.AreEqual(expectedPlatform, player.Platform);
		}
	}
}
