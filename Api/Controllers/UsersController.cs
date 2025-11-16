using Api.BusinessLogic.Dto;
using Api.BusinessLogic.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LoginRequest = Api.BusinessLogic.Dto.LoginRequest;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController: ControllerBase
{
    private readonly IUserService _UserService;

    public UsersController(IUserService userService)
    {
        _UserService = userService;
    }
    
    [HttpPost]
    [Route("authenticate")]
    [AllowAnonymous]
    public async Task<IActionResult> AuthenticateUse([FromBody] UserDto userDto)
    {
        var id = await _UserService.CreateUser(userDto);
        return Created(id.ToString(), userDto);
    }

    [HttpPut]
    [Route("activate")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ActivateUserAccount([FromQuery(Name = "mail")]string mail)
    {
        var id = await _UserService.ActivateUserAccount(mail);
        return Ok(id);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
       var loginResponse = await _UserService.LoginUser(loginRequest);
       return Ok(loginResponse);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUsers()
    {
        List<UserDtoResponse> users = await _UserService.GetUsers();
        return Ok(users);
    }
}