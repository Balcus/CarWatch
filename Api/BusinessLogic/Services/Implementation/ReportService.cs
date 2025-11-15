using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
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
    private readonly IAmazonS3 _s3Client;
    private readonly string _bucketName;

    public ReportService(IRepository<Report, int> repository, IMapper mapper, IConfiguration config)
    {
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
        
        //var report = _mapper.Map<Report>(entity);
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