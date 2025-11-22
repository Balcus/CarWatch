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
    
        int statusCode = StatusCodes.Status400BadRequest;    
        var errorResponse = new
        {
            Message = exceptions.Message,
            StatusCode = statusCode,
            Timestamp = DateTime.UtcNow
        };
        

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        return context.Response.WriteAsJsonAsync(errorResponse);
    }
}