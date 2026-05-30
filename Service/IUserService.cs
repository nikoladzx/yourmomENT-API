using Microsoft.AspNetCore.Mvc;
using yourmomENT.Dto;
using yourmomENT.Models;

namespace yourmomENT.Service;

public interface IUserService
{
    Task<User?> GetBySteamIdAsync(string steamId);

    Task<bool> UserExistsAsync(string email);
}