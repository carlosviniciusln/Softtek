using System.Net;

namespace Questao5.Application.Configurations.Helpers
{
    public class CustomException
    {
        public ErrorResponse TipoMovimentacaoInvalidaResponse()
        {
            return new ErrorResponse((int)HttpStatusCode.BadRequest, "Apenas movimentações C e D são permitidas.");
        }
        public ErrorResponse ValorInvalidoResponse()
        {
            return new ErrorResponse((int)HttpStatusCode.NotAcceptable, "Informe um valor maior que 0 (zero) para uma transação.");
        }
        public ErrorResponse ContaMovimentacaoInvalidaResponse()
        {
            return new ErrorResponse((int)HttpStatusCode.NotFound, "A conta informada não foi encontrada em nossa base de dados.");
        }
        public ErrorResponse ContaNaoInformadaResponse()
        {
            return new ErrorResponse((int)HttpStatusCode.ExpectationFailed, "É necessário informar uma conta.");
        }
        public ErrorResponse ValorNaoInformadoResponse()
        {
            return new ErrorResponse((int)HttpStatusCode.ExpectationFailed, "É necessário informar uma valor.");
        }
        public ErrorResponse TipoNaoInformadoResponse()
        {
            return new ErrorResponse((int)HttpStatusCode.ExpectationFailed, "É necessário informar uma tipo de transação.");
        }
        public ErrorResponse ServerErrorResponse()
        {
            return new ErrorResponse((int)HttpStatusCode.InternalServerError, "Ocorreu um erro inesperado. Tente novamente mais tarde.");
        }
    }

    #region Exceptions
    public class TipoMovimentacaoInvalidaException : Exception
    {
        public TipoMovimentacaoInvalidaException()
        {
        }
    }

    public class ValorInvalidoException : Exception
    {
        public ValorInvalidoException()
        {
        }
    }

    public class ContaMovimentacaoInvalidaException : Exception
    {
        public ContaMovimentacaoInvalidaException()
        {
        }
    }

    public class ContaNaoInformadaException : Exception
    {
        public ContaNaoInformadaException()
        {
        }
    }

    public class ValorNaoInformadoException : Exception
    {
        public ValorNaoInformadoException()
        {
        }
    }

    public class TipoNaoInformadoException : Exception
    {
        public TipoNaoInformadoException()
        {
        }
    } 
    #endregion
}


