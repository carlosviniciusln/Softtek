
namespace Questao5.Domain.Commands.Responses
{
    public class ResponseContaCorrenteCommand
    {
        public string IdContaCorrente { get; set; }
        public long Numero { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
    }
}
