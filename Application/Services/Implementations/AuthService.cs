using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Dtos;
using Application.Services.Api;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services.Implementations;

public class AuthService(IUserRepository userRepository, IConfiguration configuration) : IAuthService
{
    public async Task<bool> Register(RegisterRequestDto registerRequest)
    {
        var dbUser = await userRepository.GetByLoginAsync(registerRequest.Login);
        
        if (dbUser is not null)
        {
            throw new LibraryApplicationException(HttpStatusCode.UnprocessableContent, "Account with this login already exist");
        }
        
        var user = new User
        {
            Username = registerRequest.Username,
            Login = registerRequest.Login,
            Role = "user"
        };

        using (var hmac = new HMACSHA512())
        {
            user.PasswordKey = hmac.Key;
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerRequest.Password));
        }

        await userRepository.CreateAsync(user);
        
        return true;
    }
    
    public async Task<TokenDto> Login(string login, string password)
    {
        var dbUser = await userRepository.GetByLoginAsync(login);
        
        if (dbUser is null)
        {
            throw new LibraryApplicationException(HttpStatusCode.Unauthorized, "Wrong login or password");
        }
        
        if (!CheckIsPasswordCorrect(dbUser, password))
        {
            throw new LibraryApplicationException(HttpStatusCode.Unauthorized, "Wrong login or password");
        }

        var encryptedAccessToken = GenerateEncryptedAccessToken(dbUser);
        var refreshToken = GenerateRefreshToken();
        
        await userRepository.UpdateRefreshTokenByIdAsync(dbUser.Id, refreshToken);
        
        return new TokenDto(encryptedAccessToken, refreshToken, dbUser.Id, dbUser.Role);
    }

    public async Task<TokenDto> RegenerateAccessAndRefreshTokens(string? oldRefreshToken)
    {
        if (oldRefreshToken is null)
        {
            throw new LibraryApplicationException(HttpStatusCode.Unauthorized, "Refresh token is invalid");
        }
        
        var dbUser = await userRepository.GetByRefreshTokenAsync(oldRefreshToken);
        
        if (dbUser is null)
        {
            throw new LibraryApplicationException(HttpStatusCode.Unauthorized, "Refresh token is invalid");
        }
        
        var encryptedAccessToken = GenerateEncryptedAccessToken(dbUser);
        var refreshToken = GenerateRefreshToken();
        
        await userRepository.UpdateRefreshTokenByIdAsync(dbUser.Id, refreshToken);
        
        return new TokenDto(encryptedAccessToken, refreshToken, dbUser.Id, dbUser.Role);
    }

    private string GenerateEncryptedAccessToken(User user)
    {
        var claims = new List<Claim>
        {
            new("Id", user.Id.ToString()),
            new(ClaimTypes.Name, user.Login),
            new(ClaimTypes.Role, user.Role)
        };
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSecurityKey"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var accessToken = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials);

         return new JwtSecurityTokenHandler().WriteToken(accessToken);
    }

    private string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }

    private bool CheckIsPasswordCorrect(User user, string password)
    {
        byte[] actualHash;
        using (var hmac = new HMACSHA512(user.PasswordKey))
        {
            actualHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        return user.PasswordHash.SequenceEqual(actualHash);
    }
}