using System.Security.Claims;
using System.Text;
using Api.BusinessLogic.Dto;
using Api.BusinessLogic.Services.Abstraction;
using Api.DataAccess.Abstractions;
using Api.DataAccess.Entities;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Api.DataAccess.Configuration;
using Api.DataAccess.Enums;
using Microsoft.AspNetCore.Identity;

namespace Api.BusinessLogic.Services.Implementation;

public class UserService : IUserService
{
    private readonly IRepository<User, int> _userRepository;
    
    private readonly IUserRepository _iUserRepository;
    
    private readonly IMapper _mapper;
    
    private readonly IConfiguration _configuration;
    private readonly HashingServiceImpl _hasher;
    private readonly EmailProducer _emailProducer;

    public UserService(IRepository<User, int> userRepository, IUserRepository iUserRepository, IMapper mapper, IConfiguration configuration, HashingServiceImpl hasher, EmailProducer emailProducer)
    {
        _configuration = configuration;
        _userRepository = userRepository;
        _iUserRepository = iUserRepository;
        _mapper = mapper;
        _hasher = hasher;
        _emailProducer = emailProducer;
    }
    
    public async Task<int> CreateUser(UserDto userDto)
    {
        string hashedPassword = _hasher.HashPassword(userDto.Password);
        userDto.Password = hashedPassword;
        var user = _mapper.Map<User>(userDto);
        var existingUser = await _iUserRepository.GetByEmailAsync(userDto.Mail);
        if(existingUser != null) throw new Exception("Email already exists");
        
        var id = await _userRepository.CreateAsync(user); 
        
        await _emailProducer.PublishEmail(userDto.Mail);
        return id;
    }

    public async Task<List<UserDtoResponse>> GetUsers()
    {
        IList<User> users = await _userRepository.GetAllAsync();
        List<UserDtoResponse> userList = _mapper.Map<List<UserDtoResponse>>(users);
        return userList;
    }

    public async Task<LoginResponse> LoginUser(LoginRequest loginRequest)
    {
        if(string.IsNullOrEmpty(loginRequest.Mail)|| string.IsNullOrEmpty(loginRequest.Password))
        {
            throw new Exception("Email or password is incorrect");
        }
        
        var existingUser = await _iUserRepository.GetByEmailAsync(loginRequest.Mail);

        if (existingUser == null) throw new Exception("Email or password is incorrect");
        
        bool result = _hasher.VerifyPassword(existingUser.Password, loginRequest.Password);

        if(!result) throw new Exception("Password is incorrect");

        User user = await _iUserRepository.GetByEmailAsync(loginRequest.Mail);
        
        var token = GenerateJwtToken(loginRequest.Mail, user.Role);

        return new LoginResponse
        {
            token = token
        };
    }

    private string GenerateJwtToken(string email,Role role)
    {
        if(_configuration["scret"] == null ) throw new Exception("Can not create JWT");
        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["scret"]));
        
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim("as", email), // Your 'as' claim
            new Claim("role",role.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(JwtRegisteredClaimNames.Aud, _configuration["Jwt:Audience"]),
            new Claim(JwtRegisteredClaimNames.Iss, _configuration["Jwt:Issuer"]),
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Issuer"],
            audience: _configuration["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(24),
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}