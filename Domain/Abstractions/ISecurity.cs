using Domain.Entities;

namespace Domain.Abstractions;

public interface ISecurity
{
    public string GenerateEncryptedAccessToken(User user, string securityKey);
    public string GenerateRefreshToken();
    public bool CheckIsPasswordCorrect(User user, string password);
}