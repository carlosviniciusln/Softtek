using Questao5.Domain.Commands.Requests;
using Questao5.Domain.Enumerators;

namespace Questao5.Domain.Validations
{
    public class RegistraMovimentacaoValidation
    {
        private static bool isValid = false;

        public bool IsValid(RegistraMovimentacaoRequest request)
        {

            isValid = ValidarEntradasNulas(request);
            isValid = ValidarConta(request);
            isValid = ValidarTipoMovimentacao(request);
            isValid = ValidarValor(request);

            return isValid;
        }

        private static bool ValidarEntradasNulas(RegistraMovimentacaoRequest request)
        {
            if (request.IdConta == null)
            {
                throw new Exception("IdContaException");
            }
            else if (request.Valor == null)
            {
                throw new Exception("ValorException");
            }
            else if (request.Tipo == null)
            {
                throw new Exception("TipoException");
            }
            else
            {
                isValid = true;
            }

            return isValid;
        }

        private static bool ValidarValor(RegistraMovimentacaoRequest request)
        {
            if (request.Valor > 0)
            {
                isValid = true; 
            }
            else
            {
                throw new Exception("ValorInvalidoException");
            }

            return isValid;
        }

        private static bool ValidarTipoMovimentacao(RegistraMovimentacaoRequest request)
        {
            if (request.Tipo == TipoMovimentacaoEnum.C.ToString() || request.Tipo == TipoMovimentacaoEnum.D.ToString())
            {
                isValid = true;
            }
            else
            {
                throw new Exception("TipoMovimentacaoInvalidaException");
            }

            return isValid;
        }

        private static bool ValidarConta(RegistraMovimentacaoRequest request)
        {
            if (request.IdConta != Guid.Empty)
            {
                isValid = true;
            }
            else
            {
                throw new Exception("ContaMovimentacaoInvalidaException");
            }

            return isValid;
        }
    }
}
