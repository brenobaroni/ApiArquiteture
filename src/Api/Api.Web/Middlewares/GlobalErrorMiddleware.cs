using System.Net;
using System.Text.Json;

namespace Api.Web.Middlewares
{
    public class GlobalErrorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorMiddleware> _logger;

        public GlobalErrorMiddleware(RequestDelegate next, ILogger<GlobalErrorMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled error: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var response = exception switch
            {
                InvalidOperationException => new ErrorResponseModel("BadRequest", "Invalid operation", exception.Message),
                ArgumentException => new ErrorResponseModel("BadRequest", "Invalid argument", exception.Message),
                UnauthorizedAccessException => new ErrorResponseModel("Unauthorized", "Unauthorized access", exception.Message),
                _ => new ErrorResponseModel("InternalServerError", "Internal server error", "An unexpected error occurred")
            };

            context.Response.StatusCode = response.Type switch
            {
                "BadRequest" => (int)HttpStatusCode.BadRequest,
                "Unauthorized" => (int)HttpStatusCode.Unauthorized,
                "NotFound" => (int)HttpStatusCode.NotFound,
                _ => (int)HttpStatusCode.InternalServerError
            };

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response, jsonOptions));
        }
    }
}