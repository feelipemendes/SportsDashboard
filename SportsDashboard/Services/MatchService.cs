using Newtonsoft.Json;
using SportsDashboard.Models;

namespace SportsDashboard.Services
{
    public class MatchService : IMatchService
    {
        private readonly HttpClient _httpClient;

        public MatchService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        public async Task<List<CompetitionMatches>> GetMatchesForCompetitions(List<int> competitionIds, string status)
        {
            var tasks = competitionIds.Select(async id =>
            {
                var url = $"/v4/competitions/{id}/matches?status={status}";
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var competitionMatches = JsonConvert.DeserializeObject<CompetitionMatches>(content);


                    return competitionMatches?.matches?.Any() == true ? competitionMatches : null;
                }

                return null;
            });

            var results = await Task.WhenAll(tasks);
            return results
                    .Where(r => r?.matches != null && r.matches.Any())
                    .ToList();
        }
    }
}
