using DundeeUltraTournament.Core.Models;

namespace DundeeUltraTournament.Core.Interfaces
{
	public interface IWarzoneService
	{
		PlayerMatchInfo GetPlayerMatchInfo(string username, string platform);
		TeamResult GetTeamResult(string matchId, int placement);
	}
}
