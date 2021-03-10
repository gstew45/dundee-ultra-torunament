using DundeeUltraTournament.Core.Interfaces;
using DundeeUltraTournament.Core.Interfaces.Stores;
using DundeeUltraTournament.Services;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO.Abstractions;

namespace DundeeUltraTournament.Tests.Services
{
	[TestClass]
	public class FilePlayerServiceTests
	{
		private IPlayerStore m_playerStore;
		private FilePlayerService m_testSubject;

		[TestInitialize]
		public void Init()
		{
			m_playerStore = A.Fake<IPlayerStore>();
			m_testSubject = new FilePlayerService(m_playerStore);
		}

		[TestMethod]
		public void PlayerService_Can_Be_Constructed()
		{
			Assert.IsNotNull(m_testSubject);
		}

		[TestMethod]
		public void PlayerService_Implments_IPlayerService()
		{
			Assert.IsInstanceOfType(m_testSubject, typeof(IPlayerService));
		}

		[TestMethod]
		public void GetRegisteredPlayers_Gets_Registered_Players_From_Store()
		{
			m_testSubject.GetRegisteredPlayers();

			A.CallTo(() => m_playerStore.GetRegisteredPlayers()).MustHaveHappenedOnceExactly();
		}
	}
}
