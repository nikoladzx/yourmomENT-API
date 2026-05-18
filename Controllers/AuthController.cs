using AspNet.Security.OpenId.Steam;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using yourmomENT.Dto;
using yourmomENT.Service;

namespace yourmomENT.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IUserService userService, ISteamService steamServiceUtils) : ControllerBase
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
    
    [HttpGet("steam")]
    public IActionResult SteamLogin()
    {
        var properties = new AuthenticationProperties
        {
            RedirectUri = Url.Action(nameof(SteamResponse))
        };

        return Challenge(
            properties,
            SteamAuthenticationDefaults.AuthenticationScheme);
    }

    [HttpGet("steam/callback")]
    public async Task<IActionResult> SteamResponse()
    {
        var result =
            await HttpContext.AuthenticateAsync("Cookies");

        if (!result.Succeeded)
            return Unauthorized();

        var principal = result.Principal;
        
        var steamIdUrl = principal?
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var username = principal?.Identity?.Name;

        if (steamIdUrl == null)
            return Unauthorized();

        var steamId = steamIdUrl.Split('/').Last();

        var user = await steamServiceUtils.GetSteamUserAsync(steamId);

        if (user == null)
        {
            return Unauthorized();
        }
        
        var token = await userService.SteamLoginAsync(user);

        return Redirect($"http://localhost:3000/auth-success?token={token}");
    }
}