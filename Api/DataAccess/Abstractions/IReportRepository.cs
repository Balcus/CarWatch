using Api.DataAccess.Entities;

namespace Api.DataAccess.Abstractions;

public interface IReportRepository
{
    Task<List<Report>> GetAllAsyncByUserId(int userId);
}