
namespace Questao2.models
{
    internal class FootballMatch
    {
        public int page { get; set; }
        public int per_page { get; set; }
        public int total { get; set; }
        public TotalPages pages { get; set; }
        public List<Match> data { get; set; }
    }
}