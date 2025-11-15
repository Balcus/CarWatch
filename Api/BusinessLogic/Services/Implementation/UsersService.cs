using Api.BusinessLogic.Dto;
using Api.BusinessLogic.Services.Abstraction;
using Api.DataAccess.Abstractions;
using Api.DataAccess.Entities;
using Api.DataAccess.Security;

namespace Api.BusinessLogic.Services.Implementation;

public class UserService : IUserService
{
    private readonly IRepository<User, int> _userRepository;
    
    private readonly IUserRepository _iUserRepository;

    public UserService(IRepository<User, int> userRepository, IUserRepository iUserRepository)
    {
        _userRepository = userRepository;
        _iUserRepository = iUserRepository;
    }
    
    public async Task<int> CreateUser(UserDto userDto)
    {
        string hashedPassword = HashingService.HashPassword(userDto.Password);
        var existingUser = await _iUserRepository.GetByEmailAsync(userDto.Email);
        if(existingUser != null) throw new Exception("Email already exists");
        
        var id = await _userRepository.CreateAsync(new User()
        {
            Password = hashedPassword,
            Name = userDto.Name,
            Mail = userDto.Email,
        }); 
        return id;
    }

    public async Task<List<UserDtoResponse>> GetUsers()
    {
        IEnumerable<User> users = await _userRepository.GetAllAsync();
        List<UserDtoResponse> userList = new List<UserDtoResponse>();
        foreach (var user in users)
        {
            UserDtoResponse userDtoResponse = new UserDtoResponse
            {
                Id = user.Id,
                Name = user.Name,
                Mail = user.Mail
            };
            userList.Add(userDtoResponse);
        }
        return userList;
    }
}