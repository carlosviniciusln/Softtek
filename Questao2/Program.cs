using Microsoft.Extensions.DependencyInjection;
using Questao2.services;

public class Program
{
    public static void Main()
    {
        ServiceProvider services = new ServiceCollection()
            .AddHttpClient()
            .AddScoped<IFootballMatchesService, FootballMatchesService>()
            .BuildServiceProvider();

        var serviceMatches = services.GetService<IFootballMatchesService>();

        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = serviceMatches.totalGoals(teamName,year).Result;

        Console.WriteLine(serviceMatches.montaResposta(totalGoals, teamName, year));

        teamName = "Chelsea";
        year = 2014;
        totalGoals = serviceMatches.totalGoals(teamName, year).Result;

        Console.WriteLine(serviceMatches.montaResposta(totalGoals, teamName, year));

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }
}