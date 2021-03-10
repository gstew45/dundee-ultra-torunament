using DundeeUltraTournament.Core.Interfaces.Stores;
using DundeeUltraTournament.Core.Models;
using DundeeUltraTournament.Persistence.Parsers;
using System.Collections.Generic;
using System.IO.Abstractions;

namespace DundeeUltraTournament.Persistence
{
	public class FilePlayerStore : IPlayerStore
	{
		private readonly IFileSystem m_fileSystem;
		private readonly IPlayersParser m_playersParser;

		public FilePlayerStore(IFileSystem fileSystem, IPlayersParser playersParser)
		{
			m_fileSystem = fileSystem;
			m_playersParser = playersParser;
		}

		public IEnumerable<Player> GetRegisteredPlayers()
		{
			string currentDirectory = m_fileSystem.Directory.GetCurrentDirectory();

			string inputPath = $@"{currentDirectory}\Data\registeredPlayers.json";
			string json = m_fileSystem.File.ReadAllText(inputPath);

			return m_playersParser.GetPlayers(json);
		}
	}
}
