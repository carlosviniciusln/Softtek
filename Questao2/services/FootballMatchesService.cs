using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Questao2.models;

namespace Questao2.services
{
    internal class FootballMatchesService : IFootballMatchesService
    {
        private readonly string baseUrl = "https://jsonmock.hackerrank.com/api/football_matches?";
        private readonly HttpClient _httpClient;

        public FootballMatchesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> totalGoals(string team, int year)
        {
            int goalsAsTeam1 = await CalculateGoalsAsTeam1(team, year);
            int goalsAsTeam2 = await CalculateGoalsAsTeam2(team, year);
            return goalsAsTeam1 + goalsAsTeam2;
        }

        public string montaResposta(int totalGoals, string teamName, int year)
        {
            return ("Team " + teamName + " scored " + totalGoals + " goals in " + year);
        }

        private async Task<int> CalculateGoalsAsTeam1(string team, int year)
        {
            int goals = 0;

            for (int page = 1; page <= GetTotalPages(team, year).Result; page++)
            {
                var response = await _httpClient.GetAsync(baseUrl + "team1=" + team + "&year=" + year + "&page=" + page);
                var responseBody = await response.Content.ReadAsStringAsync();
                FootballMatch footballMatch = JsonConvert.DeserializeObject<FootballMatch>(responseBody);
                foreach (var item in footballMatch.data)
                {
                    goals += item.team1goals;
                }
            }

            return goals;
        }

        private async Task<int> CalculateGoalsAsTeam2(string team, int year)
        {
            int goals = 0;

            for (int page = 1; page <= GetTotalPages(team, year).Result; page++)
            {
                var response = await _httpClient.GetAsync(baseUrl + "team2=" + team + "&year=" + year + "&page=" + page);
                var responseBody = await response.Content.ReadAsStringAsync();
                FootballMatch footballMatch = JsonConvert.DeserializeObject<FootballMatch>(responseBody);
                foreach (var item in footballMatch.data)
                {
                    goals += item.team2goals;
                }
            }

            return goals;
        }

        private async Task<int> GetTotalPages(string team, int year)
        {
            var response = await _httpClient.GetAsync(baseUrl + "team1=" + team + "&year=" + year);
            var responseBody = await response.Content.ReadAsStringAsync();
            TotalPages totalPages = JsonConvert.DeserializeObject<TotalPages>(responseBody);
            return totalPages.total_pages;
        }

    }
}
