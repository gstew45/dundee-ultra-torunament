using DundeeUltraTournament.Core.Interfaces.Stores;
using DundeeUltraTournament.Core.Models;
using DundeeUltraTournament.Persistence;
using DundeeUltraTournament.Persistence.Parsers;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;

namespace DundeeUltraTournament.Tests.Persistence
{
	[TestClass]
	public class FilePlayerStoreTests
	{
		private IFileSystem m_fileSystem;
		private IPlayersParser m_playersParser;
		private FilePlayerStore m_testSubject;

		[TestInitialize]
		public void Init()
		{
			m_fileSystem = A.Fake<IFileSystem>();
			m_playersParser = A.Fake<IPlayersParser>();
			m_testSubject = new FilePlayerStore(m_fileSystem, m_playersParser);
		}

		[TestMethod]
		public void FilePlayerStore_Can_Be_Constructed()
		{
			Assert.IsNotNull(m_testSubject);
		}

		[TestMethod]
		public void FilePlayerStore_Implments_IPlayerStore()
		{
			Assert.IsInstanceOfType(m_testSubject, typeof(IPlayerStore));
		}

		[TestMethod]
		public void GetRegisteredPlayers_Gets_Registered_Players__Data_From_Store()
		{
			m_testSubject.GetRegisteredPlayers();

			A.CallTo(() => m_fileSystem.File.ReadAllText(A<string>._)).MustHaveHappenedOnceExactly();
		}

		[TestMethod]
		public void GetRegisteredPlayers_ParsesPlayerData_ReturnsPlayers()
		{
			List<Player> expectedPlayers = new List<Player>();
			string playerData = "ExamplePlayerData";
			A.CallTo(() => m_fileSystem.File.ReadAllText(A<string>._)).Returns(playerData);

			A.CallTo(() => m_playersParser.GetPlayers(playerData)).Returns(expectedPlayers);

			List<Player> players = m_testSubject.GetRegisteredPlayers().ToList();

			CollectionAssert.AreEqual(expectedPlayers, players);
		}
	}
}
