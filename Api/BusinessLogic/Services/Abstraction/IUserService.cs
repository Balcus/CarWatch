using Api.BusinessLogic.Dto;

namespace Api.BusinessLogic.Services.Abstraction;

public interface IUserService
{
    Task<int> CreateUser(UserDto userDto);

    Task<List<UserDtoResponse>> GetUsers();
    
    Task<LoginResponse> LoginUser(LoginRequest loginRequest);
    Task<int> ActivateUserAccount(string email);
}