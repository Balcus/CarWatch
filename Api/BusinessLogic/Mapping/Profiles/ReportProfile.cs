using Api.BusinessLogic.Dto;
using Api.DataAccess.Entities;
using AutoMapper;

namespace Api.BusinessLogic.Mapping.Profiles;

public class ReportProfile : Profile
{
    public ReportProfile()
    {
        CreateMap<Report, ReportDto>();
        CreateMap<ReportDto, Report>();
    }
}