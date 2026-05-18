using yourmomENT.Data;
using yourmomENT.Models;
using yourmomENT.Utils;
using Microsoft.EntityFrameworkCore;
using yourmomENT.Dto;

namespace yourmomENT.Service;

public class UserService(AppDbContext context, ITokenGenerator tokenGenerator) : IUserService
{
    public async Task<string> RegisterAsync(RegisterDto dto)
    {
        var exists = await context.Users.AnyAsync(x => x.Email == dto.Email);
        if (exists)
            throw new InvalidOperationException("User already exists");

        var user = new User
        {
            Username = dto.Username,
            Email = dto.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(dto.Password)
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();

        return tokenGenerator.GenerateToken(
            user.Id.ToString(),
            user.Email
        );
    }

    public async Task<string> LoginAsync(LoginDto dto)
    {
        var user = await context.Users
            .FirstOrDefaultAsync(x => x.Email == dto.Email);

        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            throw new UnauthorizedAccessException("Invalid credentials");

        return tokenGenerator.GenerateToken(
            user.Id.ToString(),
            user.Email
        );
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await context.Users.FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<bool> UserExistsAsync(string email)
    {
        return await context.Users.AnyAsync(x => x.Email == email);
    }
    
    public async Task<string> SteamLoginAsync(SteamUserDto steamUserDto)
    {
        var user = await context.Users
            .FirstOrDefaultAsync(x => x.SteamId == steamUserDto.SteamId);

        if (user == null)
        {
            user = new User
            {
                SteamId = steamUserDto.SteamId,
                Username = steamUserDto.Username,
                AvatarUrl = steamUserDto.AvatarUrl
            };

            context.Users.Add(user);
            await context.SaveChangesAsync();
        }
        else
        {
            user.Username = steamUserDto.Username;
            user.AvatarUrl = steamUserDto.AvatarUrl;

            await context.SaveChangesAsync();
        }

        return tokenGenerator.GenerateToken(
            user.Id.ToString(),
            user.Username
        );
    }

    public async Task<User?> GetBySteamIdAsync(string steamId)
    {
        return await context.Users
            .FirstOrDefaultAsync(x => x.SteamId == steamId);
    }
}