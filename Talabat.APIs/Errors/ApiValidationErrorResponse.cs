namespace Talabat.APIs.Errors
{
    public class ApiValidationErrorResponse :Apiresponse
    {
        public IEnumerable<string> Errors { get; set; }
        public ApiValidationErrorResponse(): base(400)
        {
            Errors = new List<string>();
        }
    }
}
