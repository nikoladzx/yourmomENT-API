using yourmomENT.Dto;
using yourmomENT.DTO;
using yourmomENT.Models;

namespace yourmomENT.Service;

public interface IUserService
{
    Task<string> RegisterAsync(RegisterDTO dto);

    Task<string> LoginAsync(LoginDto dto);

    Task<User?> GetByEmailAsync(string email);

    Task<bool> UserExistsAsync(string email);
}