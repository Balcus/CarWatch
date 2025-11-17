using Api.DataAccess.Enums;

namespace Api.BusinessLogic.Dto;

public class ReportResponseDto
{
    public decimal Latitude { get; init; }
    
    public decimal Longitude { get; init; }
    
    public string? Description { get; init; }
    
    public int UserId { get; init; }
    
    public string Status { get; init; }
    
}