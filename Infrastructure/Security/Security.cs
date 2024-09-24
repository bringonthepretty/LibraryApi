using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Security;

public class Security : ISecurity
{
    public string GenerateEncryptedAccessToken(User user, string securityKey)
    {
        var claims = new List<Claim>
        {
            new("Id", user.Id.ToString()),
            new(ClaimTypes.Name, user.Login),
            new(ClaimTypes.Role, user.Role)
        };
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var accessToken = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(accessToken);
    }

    public string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }

    public bool CheckIsPasswordCorrect(User user, string password)
    {
        byte[] actualHash;
        using (var hmac = new HMACSHA512(user.PasswordKey))
        {
            actualHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        return user.PasswordHash.SequenceEqual(actualHash);
    }
}