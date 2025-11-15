using Api.BusinessLogic.Dto;
using Api.BusinessLogic.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public async Task<IActionResult> GetAllAsync()
    {
        var  reports = await _reportService.GetAllAsync();
        return Ok(reports);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateAsync([FromForm] string reportJson, [FromForm] IFormFile image)
    {
        if (string.IsNullOrEmpty(reportJson))
            return BadRequest("Report data is required");
        var entity = System.Text.Json.JsonSerializer.Deserialize<ReportDto>(reportJson,
            new System.Text.Json.JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        if (entity == null)
            return BadRequest("Invalid report data");
        var id = await _reportService.CreateAsync(entity, image);
        return Ok(id);
    }
}