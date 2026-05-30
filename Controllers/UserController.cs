using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using yourmomENT.Service;

namespace yourmomENT.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> Me()
    {
        var steamIdUrl = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (steamIdUrl == null)
            return Unauthorized();

        var steamId = steamIdUrl.Split('/').Last();
        
        var user = await userService.GetBySteamIdAsync(steamId);

        if (user == null)
            return NotFound();

        return Ok(user);
    }
}