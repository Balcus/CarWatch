namespace Api.DataAccess.Exceptions;

public class ErrorResponse
{
    public string Message { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public int StatusCode { get; set; }
}