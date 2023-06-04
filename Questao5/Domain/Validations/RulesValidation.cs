using Questao5.Domain.Commands.Requests;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Commands.Responses;

namespace Questao5.Domain.Validations
{
    public class RulesValidation
    {
        private static bool isValid = false;
        
        public bool IsValidForAccountMovement(RequestAccountMovementCommand request, ResponseContaCorrenteCommand account)
        {
            isValid = ValidateInputRegisteredAccount(account);
            isValid = ValidateInputValorIsBiggerThenZero(request);
            isValid = ValidateInputTipoMovimentacao(request);
            isValid = ValidateActiveAccount(account);
            return isValid;
        }

        public bool IsValidForSaldoContaCorrente(ResponseContaCorrenteCommand account)
        {
            isValid = ValidateInputRegisteredAccount(account);
            isValid = ValidateActiveAccount(account);
            return isValid;
        }

        #region private functions
        ///<summary>
        ///rule for INVALID_ACCOUNT
        ///</summary>
        private static bool ValidateInputRegisteredAccount(ResponseContaCorrenteCommand account)
        {
            if(account != null)
            {
                isValid = true;
            }
            else
            {
                throw new Exception("ContaMovimentacaoInvalidaException");
            }
            return isValid;
        }
        ///<summary>
        ///rule for INVALID_VALUE
        ///</summary>
        private static bool ValidateInputValorIsBiggerThenZero(RequestAccountMovementCommand request)
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
        ///<summary>
        ///rule for INVALID_TYPE
        ///</summary>
        private static bool ValidateInputTipoMovimentacao(RequestAccountMovementCommand request)
        {
            if (request.Tipo.Equals(TipoMovimentacaoEnum.C.ToString()) || request.Tipo.Equals(TipoMovimentacaoEnum.D.ToString()))
            {
                isValid = true;
            }
            else
            {
                throw new Exception("TipoMovimentacaoInvalidaException");
            }

            return isValid;
        }
        ///<summary>
        ///rule for INACTIVE_ACCOUNT
        ///</summary>
        private static bool ValidateActiveAccount(ResponseContaCorrenteCommand account)
        {
            if (account.Ativo)
            {
                isValid = true;
            }
            else
            {
                throw new Exception("ContaMovimentacaoInativaException");
            }

            return isValid;
        } 
        #endregion
    }
}
