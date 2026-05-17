using Microsoft.AspNetCore.Mvc;
using yourmomENT.Dto;
using yourmomENT.Service;

namespace yourmomENT.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IUserService userService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var exists = await userService.UserExistsAsync(registerDto.Email);

        if (exists)
        {
            return Conflict(new { message = "User already exists" });
        }

        var token = await userService.RegisterAsync(registerDto);

        if (string.IsNullOrWhiteSpace(token))
        {
            return StatusCode(500, new { message = "Registration failed" });
        }

        return Ok(new
        {
            message = "User registered successfully",
            token = token,
            user = new
            {
                registerDto.Username,
                registerDto.Email
            }
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var token = await userService.LoginAsync(loginDto);

        if (string.IsNullOrWhiteSpace(token))
        {
            return Unauthorized(new { message = "Invalid credentials" });
        }

        return Ok(new { token });
    }
}