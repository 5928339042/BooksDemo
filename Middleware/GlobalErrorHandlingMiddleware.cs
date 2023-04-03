using System.Net;
using System.Text.Json;
using BooksDemo.Helpers;

namespace BooksDemo.Middleware;

public class GlobalErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public GlobalErrorHandlingMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception appError)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            switch (appError)
            {
                case RepositoryException:
                    // Custom repository/service error
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case KeyNotFoundException:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                default:
                    // An unhandled error in the application
                    _logger.LogError(appError, "An unhandled error");
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var result = JsonSerializer.Serialize(new { errMessage = appError.Message });
            await response.WriteAsync(result);
        }
    }

}