namespace Talabat.APIs.Errors
{
    public class ApiExceptionResponse : Apiresponse
    {
        public string? Details { get; set; }

        public ApiExceptionResponse(int statuscode, string? message = null, string? details = null)
            : base(statuscode , message)
        {
            Details = details;
        }
    }
}
