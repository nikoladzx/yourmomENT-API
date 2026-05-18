using System.Text.Json.Serialization;

namespace yourmomENT.Dto;

public class SteamUserDto
{
    [JsonPropertyName("steamid")]
    public required string SteamId { get; set; }

    [JsonPropertyName("personaname")]
    public required string Username { get; set; }

    [JsonPropertyName("avatar")]
    public string? AvatarUrl { get; set; }
}