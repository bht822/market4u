using System.Collections.Generic;

namespace API.Errors
{
    public class ApiValidationErrorResponse : ApiResponse
    {
        public ApiValidationErrorResponse() : base(400)
        {

        }
        // Upon a validation error the modal state returns the list of errors
        public IEnumerable<string> Errors { get; set; } 
    }
}