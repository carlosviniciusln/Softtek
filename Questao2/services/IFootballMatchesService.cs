using Questao2.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questao2.services
{
    internal interface IFootballMatchesService
    {
        public Task<int> totalGoals(string team, int year);
        public string montaResposta(int totalGoals, string teamName, int year);
    }
}
