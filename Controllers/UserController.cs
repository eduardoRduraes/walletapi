using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WalletAPI.DTOs;
using WalletAPI.Services;

namespace WalletAPI.Controllers;


[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    private readonly TokenService _tokenService;

    public UserController(UserService userService, TokenService tokenService)
    {
        _userService = userService;
        _tokenService = tokenService;
    }

    [HttpPost]
    public IActionResult Register(CreateUserDTO createUserDTO)
    {
        try
        {
            var user = _userService.CreateUser(createUserDTO.Username, createUserDTO.Password);
            return CreatedAtAction(nameof(Register), new { id = user.Id }, new { id = user.Id });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    
    [HttpPost("login")]
    public IActionResult Login([FromBody] CreateUserDTO loginDTO)
    {
        var user = _userService.AuthenticateUser(loginDTO.Username, loginDTO.Password);
        if (user == null)
        {
            return Unauthorized(new { message = "Username or password is incorrect" });
        }

        var token = _tokenService.GenerateToken(user.Id, user.Username);
        return Ok(new { token = token });
    }
}