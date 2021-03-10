using DundeeUltraTournament.Core.Interfaces;

namespace DundeeUltraTournament.DataPoller
{
	public class PollData
	{
		private readonly IDataGenerator m_dataGenerator;

		public PollData(IDataGenerator dataGenerator)
		{
			m_dataGenerator = dataGenerator;
		}

		public void Poll()
		{
			m_dataGenerator.GenerateData();
		}
	}
}
