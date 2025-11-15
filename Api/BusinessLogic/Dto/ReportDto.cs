namespace Api.BusinessLogic.Dto;

public record ReportDto
{
    public decimal Latitude { get; init; }
    
    public decimal Longitude { get; init; }
    
    public string? Description { get; init; }
    
    public int UserId { get; init; }
}