using Microsoft.AspNetCore.Identity;

namespace yourmomENT.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        
        public string? AvatarUrl { get; set; }
        
        public string? SteamId { get; set; }
    }
}
