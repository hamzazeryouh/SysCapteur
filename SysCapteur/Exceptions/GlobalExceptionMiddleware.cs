using Newtonsoft.Json;
using System.Net;

namespace SysCapteur.Exceptions
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // Call the next middleware
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Log the exception
            _logger.LogError(exception, "An unexpected error occurred.");

            var response = new Error
            {
                ErrorCode = (int)HttpStatusCode.InternalServerError,
                ErrorMessage = "An unexpected error occurred.",
                ErrorSource = exception.Message // You may not want to expose the details in production
            };

            // Set the response type to application/json
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = response.ErrorCode;

            // Return the response as a JSON string
            var result = JsonConvert.SerializeObject(response);
            return context.Response.WriteAsync(result);
        }
    }
}

