using AspNet.Security.OpenId.Steam;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using yourmomENT.Dto;
using yourmomENT.Service;

namespace yourmomENT.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(
    ISteamService steamServiceUtils,
    IOptions<FrontendOptions> frontendOptions
) : ControllerBase
{
    private readonly string _frontendUrl = frontendOptions.Value.BaseUrl;

    [HttpGet("steam")]
    public IActionResult SteamLogin()
    {
        var properties = new AuthenticationProperties
        {
            RedirectUri = Url.Action(nameof(SteamResponse))
        };

        return Challenge(properties, SteamAuthenticationDefaults.AuthenticationScheme);
    }

    [HttpGet("steam/callback")]
    public async Task<IActionResult> SteamResponse()
    {
        var result = await HttpContext.AuthenticateAsync(
            SteamAuthenticationDefaults.AuthenticationScheme);

        if (!result.Succeeded)
            return Unauthorized();

        var steamIdUrl = result.Principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (steamIdUrl == null)
            return Unauthorized();

        var steamId = steamIdUrl.Split('/').Last();

        var user = await steamServiceUtils.GetSteamUserAsync(steamId);

        if (user == null)
            return Unauthorized();

        return Redirect($"{_frontendUrl}/auth/steam-success");
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync("Cookies");
        return Ok();
    }
}