using Api.BusinessLogic.Dto;

namespace Api.BusinessLogic.Services.Abstraction;

public interface IReportService
{
    public Task<List<ReportResponseDto>> GetAllByUserIdAsync();
}