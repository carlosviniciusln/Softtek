namespace Questao5.Application.Configurations.Helpers
{
    public class ErrorResponse
    {
        public int ErrorCode { get; set; }
        public string Message { get; set; }
        public string ValidationError { get; set; }
        public DateTime TimeStamp { get; set; }

        public ErrorResponse(int code, string message, string validationError)
        {
            ErrorCode = code;
            Message = message;
            TimeStamp = DateTime.Now;
            ValidationError = validationError;
        }
    }
}
