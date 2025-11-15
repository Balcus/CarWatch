using Api.BusinessLogic.Dto;
using Api.BusinessLogic.Services.Abstraction;
using Api.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> AuthenticateUse([FromBody] UserDto userDto)
    {
        var id = await _UserService.CreateUser(userDto);
        return Created(id.ToString(), userDto);
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        List<UserDtoResponse> users = await _UserService.GetUsers();
        return Ok(users);
    }
}