using Api.DataAccess.Abstractions;
using Api.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.DataAccess.Repositories;

public class ReportRepositoy : IReportRepository
{
    private readonly DbContext _dbContext;

    public ReportRepositoy(DatabaseContext context)
    {
        _dbContext = context;
    }
    public async Task<List<Report>> GetAllAsyncByUserId(int userId)
    {
        return await _dbContext.Set<Report>()
            .Where(r => r.UserId == userId)
            .ToListAsync();
    }
}