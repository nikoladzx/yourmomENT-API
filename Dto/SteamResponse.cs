using System.Text.Json.Serialization;

namespace yourmomENT.Dto;

public class SteamResponse
{
    [JsonPropertyName("response")]
    public SteamUsersDto? Response { get; set; }
}
