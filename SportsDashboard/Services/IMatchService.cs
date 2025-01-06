using SportsDashboard.Models;

namespace SportsDashboard.Services
{
    public interface IMatchService
    {
        Task<List<CompetitionMatches>> GetMatchesForCompetitions(List<int> competitionIds, string status);

    }
}
