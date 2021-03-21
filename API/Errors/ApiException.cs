namespace API.Errors
{
    public class ApiException : ApiResponse
    {
        public ApiException(int statusCode, string message = null, string detials = null) : base(statusCode, message)
        {
            Details = detials;
        }

        public string Details { get; set; }
    }
}