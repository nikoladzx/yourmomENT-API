using System.Net.Http;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using yourmomENT.Dto;

namespace yourmomENT.Service;

public class SteamService(IConfiguration configuration, HttpClient httpClient) : ISteamService
{
    public async Task<SteamUserDto?> GetSteamUserAsync(string steamId)
    {
        var apiKey = configuration["Steam:ApiKey"];

        var url =
            "https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v2/" +
            $"?key={apiKey}&steamids={steamId}";

        var json = await httpClient.GetStringAsync(url);
        
        var result = JsonSerializer.Deserialize<SteamResponse>(json);
        
        var players = result?.Response?.SteamUsers;
        
        var player = players?.FirstOrDefault();

        return player;
    }
}