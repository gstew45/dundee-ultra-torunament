using System.Collections.Generic;

namespace DundeeUltraTournament.Core.Models
{
	public class TeamResult
	{
		public string Captain { get; set; }
		public string StartedAt { get; set; }
		public int Placement { get; set; }
		public List<MemberResult> MemberResults { get; }
	}
}
