namespace CSharpApp.Application.Exceptions
{
    public class BadRequestException : Exception
    {
        public int StatusCode { get; }

        public BadRequestException(string message) : base(message) { }

        public BadRequestException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
