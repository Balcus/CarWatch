using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using Api.BusinessLogic.Dto;
using Api.BusinessLogic.Services.Abstraction;
using Api.DataAccess.Abstractions;
using Api.DataAccess.Entities;
using AutoMapper;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Api.BusinessLogic.Services.Implementation;

public class ReportService : ICrudService<ReportDto, int>, IReportService
{
    private readonly IRepository<Report, int> _repository;
    private readonly IUserRepository _userRepository;
    private readonly IReportRepository _reportRepository;
    private readonly IMapper _mapper;
    private readonly IAmazonS3 _s3Client;
    private readonly string _bucketName;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ReportService(IRepository<Report, int> repository,
        IMapper mapper, 
        IConfiguration config, 
        IHttpContextAccessor httpContextAccessor,
        IUserRepository userRepository,
        IReportRepository reportRepository
        )
    {
        _httpContextAccessor = httpContextAccessor;
        _userRepository = userRepository;
        _reportRepository = reportRepository;
        _repository = repository;
        _mapper = mapper;
        _bucketName = config["AWS:BucketName"];
        _s3Client = new AmazonS3Client(
            config["AWS:AccessKey"],
            config["AWS:SecretKey"],
            RegionEndpoint.GetBySystemName(config["AWS:Region"])
        );
        
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
        throw new NotImplementedException();
    }

    public async Task<List<ReportResponseDto>> GetAllByUserIdAsync()
    {
        var email = _httpContextAccessor.HttpContext?.User.FindFirst("as")?.Value;
        if (email == null)
            throw new UnauthorizedAccessException("Email not found in JWT");
        var user = await _userRepository.GetByEmailAsync(email);
        if (user == null) 
            throw new UnauthorizedAccessException("User not found in JWT");
        List<Report> reports = await _reportRepository.GetAllAsyncByUserId(user.Id);
        List<ReportResponseDto> returnReports = new List<ReportResponseDto>();
        
        foreach (var report in reports)
        {
            returnReports.Add(new ReportResponseDto
            {
               Latitude = report.Latitude,
               Longitude = report.Longitude,
               Description = report.Description,
               UserId = report.UserId,
               Status = report.Status.ToString()
            });
        }
       return returnReports;
    }

    public async Task<int> CreateAsync(ReportDto entity, IFormFile image)
    {
        if (image == null || image.Length == 0)
            throw new Exception("File is empty");
        
        var fileTransferUtility = new TransferUtility(_s3Client);
        var key = $"{Guid.NewGuid()}_{image.FileName}";

        await using (var stream = image.OpenReadStream())
        { 
            await fileTransferUtility.UploadAsync(stream, _bucketName, key);
        }

        var ImageUrl = $"https://{_bucketName}.s3.amazonaws.com/{key}";

        var report = new Report()
        {
            Description = entity.Description,
            ImageUrl = ImageUrl,
            Latitude =  entity.Latitude,
            Longitude =  entity.Longitude,
            UserId = entity.UserId
        };
        
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