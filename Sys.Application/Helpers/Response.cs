using SysCapteur.Exceptions;

namespace Sys.Application.Helpers
{
    public class Response<T> : IResponse<T>
    {
        public bool Success { get; set; }  // Indicates if the request was successful
        public T Data { get; set; }        // The response data (of type T)
        public CustomException Error { get; set; }  // Error details, if any
        public int StatusCode { get; set; } // HTTP status code

        // Constructor for success response
        public Response(T data, int statusCode = 200)
        {
            Success = true;
            Data = data;
            Error = null;
            StatusCode = statusCode;
        }

        // Constructor for error response
        public Response(CustomException error, int statusCode = 400)
        {
            Success = false;
            Data = default;
            Error = error;
            StatusCode = statusCode;
        }
    }
}
