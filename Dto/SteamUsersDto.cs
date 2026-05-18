using System.Text.Json.Serialization;

namespace yourmomENT.Dto;

public class SteamUsersDto
{
    [JsonPropertyName("players")]
    public List<SteamUserDto>? SteamUsers { get; set; }
}