using yourmomENT.Data;
using yourmomENT.Models;
using Microsoft.EntityFrameworkCore;

namespace yourmomENT.Service;

public class UserService(AppDbContext context) : IUserService
{
    public async Task<User?> GetByEmailAsync(string email)
    {
        return await context.Users.FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<bool> UserExistsAsync(string email)
    {
        return await context.Users.AnyAsync(x => x.Email == email);
    }

    public async Task<User?> GetBySteamIdAsync(string steamId)
    {
        var user = await context.Users
            .FirstOrDefaultAsync(x => x.SteamId == steamId);

        return user;
    }
}