
using Microsoft.AspNetCore.Hosting.Server;

namespace Talabat.APIs.Errors
{
    public class Apiresponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public Apiresponse(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        #region Error Handling
        private string? GetDefaultMessageForStatusCode(int statusCode) //Using Switch Case
        {

          

            return statusCode switch
            {
                400 => "Bad Request",
                401 => "UnAuthorized",
                402 => "Payment Required",
                404 => "Page Not Found",
                405 => "Method Not Allowed",
                500 => "Internal Server Error",
                502 => " Bad Gateway",
                _ => null,
            };
        }

        #endregion
    }
}

