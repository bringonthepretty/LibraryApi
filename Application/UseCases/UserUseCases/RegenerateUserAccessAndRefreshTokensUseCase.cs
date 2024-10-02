using System.Net;
using Application.DependencyInjectionExtensions;
using Application.Dtos;
using Application.Exceptions;
using Domain.Abstractions;
using Microsoft.Extensions.Configuration;

namespace Application.UseCases.UserUseCases;

[Service]
public class RegenerateUserAccessAndRefreshTokensUseCase(IUserRepository userRepository, ISecurity security, IConfiguration configuration)
{
    public async Task<TokenDto> InvokeAsync(string? oldRefreshToken)
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
        
        var encryptedAccessToken = security.GenerateEncryptedAccessToken(dbUser, configuration["JwtSecurityKey"]!);
        var refreshToken = security.GenerateRefreshToken();
        
        await userRepository.UpdateRefreshTokenByIdAsync(dbUser.Id, refreshToken);
        await userRepository.SaveChangesAsync();
        
        return new TokenDto(encryptedAccessToken, refreshToken, dbUser.Id, dbUser.Role);
    }
}