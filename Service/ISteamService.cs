using yourmomENT.Dto;

namespace yourmomENT.Service;

public interface ISteamService
{
    Task<SteamUserDto?> GetSteamUserAsync(string steamId);
}