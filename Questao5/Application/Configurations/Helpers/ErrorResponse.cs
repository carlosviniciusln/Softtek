namespace Questao5.Application.Configurations.Helpers
{
    public class ErrorResponse
    {
        public int ErrorCode { get; set; }
        public string Message { get; set; }
        public DateTime TimeStamp { get; set; }

        public ErrorResponse()
        {
        }

        public ErrorResponse(int code, string message)
        {
            ErrorCode = code;
            Message = message;
            TimeStamp = DateTime.Now;
        }
    }
}
