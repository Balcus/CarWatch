using Api.BusinessLogic.Dto;
using Api.DataAccess.Entities;
using AutoMapper;

namespace Api.BusinessLogic.Mapping.Profiles;

public class UserProfile: Profile
{
    public UserProfile()
    {
        CreateMap<UserDto, User>();
        CreateMap<User, UserDtoResponse>();
    }
}