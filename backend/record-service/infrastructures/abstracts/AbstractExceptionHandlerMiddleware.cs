using System.Net;

namespace record_service.infrastructures.abstracts;

public abstract class AbstractExceptionHandlerMiddleware
{
    private readonly ILogger<AbstractExceptionHandlerMiddleware> _logger;
    private readonly RequestDelegate _next;

    protected AbstractExceptionHandlerMiddleware(ILogger<AbstractExceptionHandlerMiddleware> logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }
    
    public abstract (HttpStatusCode code, string message) GetResponse(Exception exception);
    
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "error during executing {Context}", context.Request.Path.Value);
            var response = context.Response;
            response.ContentType = "application/json";
            
            var (status, message) = GetResponse(exception);
            response.StatusCode = (int) status;
            await response.WriteAsync(message);
        }
    }
}