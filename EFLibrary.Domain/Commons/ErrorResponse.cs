namespace EFLibrary.Domain.Commons
{
    public class ErrorResponse
    {
        public string Error { get; set; }

        public int StatusCode { get; set; }

        public ErrorResponse(int statusCode, string error)
        {
            StatusCode = statusCode;
            Error = error;
        }
    }
}