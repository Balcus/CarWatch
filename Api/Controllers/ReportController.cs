using Api.BusinessLogic.Dto;
using Api.BusinessLogic.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReportController : Controller
{
    private readonly ICrudService<ReportDto, int> _reportService;

    public ReportController(ICrudService<ReportDto, int> reportService)
    {
        _reportService = reportService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var  reports = await _reportService.GetAllAsync();
        return Ok(reports);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] ReportDto entity)
    {
        var id = await _reportService.CreateAsync(entity);
        return Ok(id);
    }
}