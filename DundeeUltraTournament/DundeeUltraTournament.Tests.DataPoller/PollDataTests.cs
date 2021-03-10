using DundeeUltraTournament.Core.Interfaces;
using DundeeUltraTournament.DataPoller;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DundeeUltraTournament.Tests.DataPoller
{
	[TestClass]
	public class PollDataTests
	{
		private IDataGenerator m_dataGenerator;
		private PollData m_testSubject;

		[TestInitialize]
		public void Init()
		{
			m_dataGenerator = A.Fake<IDataGenerator>();
			m_testSubject = new PollData(m_dataGenerator);
		}

		[TestMethod]
		public void PollData_Can_Be_Constructed()
		{
			Assert.IsNotNull(m_testSubject);	
		}

		[TestMethod]
		public void Poll_GeneratesData()
		{
			m_testSubject.Poll();

			A.CallTo(() => m_dataGenerator.GenerateData()).MustHaveHappened();
		}
	}
}
