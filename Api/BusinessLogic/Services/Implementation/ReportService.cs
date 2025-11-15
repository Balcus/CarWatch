using Api.BusinessLogic.Dto;
using Api.BusinessLogic.Services.Abstraction;
using Api.DataAccess.Abstractions;
using Api.DataAccess.Entities;
using AutoMapper;

namespace Api.BusinessLogic.Services.Implementation;

public class ReportService : ICrudService<ReportDto, int>
{
    private readonly IRepository<Report, int> _repository;
    private readonly IMapper _mapper;

    public ReportService(IRepository<Report, int> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<List<ReportDto>> GetAllAsync()
    {
        var result = await _repository.GetAllAsync();
        return _mapper.Map<List<ReportDto>>(result);
    }

    public Task<ReportDto> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<int> CreateAsync(ReportDto entity)
    {
        var report = _mapper.Map<Report>(entity);
        return await _repository.CreateAsync(report);
    }

    public Task<int> UpdateAsync(int id, ReportDto entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteAsync(ReportDto entity)
    {
        throw new NotImplementedException();
    }
}