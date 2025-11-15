using System.Text.Json;


namespace Api.DataAccess.Exceptions;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exceptions)
    {
        int statusCode = 404;

        var errorResponse = new
        {
            exceptions.Message,
            statusCode,
            DateTime.UtcNow
        };

        string json = JsonSerializer.Serialize(errorResponse);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        return context.Response.WriteAsync(json);
    }
}