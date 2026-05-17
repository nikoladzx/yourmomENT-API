namespace yourmomENT.Utils;

public interface ITokenGenerator
{
    string GenerateToken(string userId, string email);
}