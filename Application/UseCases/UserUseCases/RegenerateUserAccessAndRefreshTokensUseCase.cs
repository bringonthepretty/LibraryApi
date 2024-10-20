using System.Net;
using Application.DependencyInjectionExtensions;
using Application.Dtos;
using Application.Exceptions;
using Application.Requests.Implementations.UserRequests;
using Domain.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Application.UseCases.UserUseCases;

[Service]
public class RegenerateUserAccessAndRefreshTokensUseCase(IUserRepository userRepository, ISecurity security, IConfiguration configuration)
{
    public async Task<TokenDto> InvokeAsync(RegenerateUserAccessAndRefreshTokensRequest request)
    {
        if (request.OldRefreshToken is null)
        {
            throw new LibraryApplicationException(ExceptionCode.AuthenticationError, "Refresh token is invalid");
        }
        
        var dbUser = await userRepository.GetByRefreshTokenAsync(request.OldRefreshToken);
        
        if (dbUser is null)
        {
            throw new LibraryApplicationException(ExceptionCode.AuthenticationError, "Refresh token is invalid");
        }
        
        var encryptedAccessToken = security.GenerateEncryptedAccessToken(dbUser, configuration["JwtSecurityKey"]!);
        var refreshToken = security.GenerateRefreshToken();
        
        await userRepository.UpdateRefreshTokenByIdAsync(dbUser.Id, refreshToken);
        await userRepository.SaveChangesAsync();
        
        request.ResponseCookies.Append("RefreshToken", refreshToken, new CookieOptions(){HttpOnly = true, Secure = true});
        
        return new TokenDto(encryptedAccessToken, dbUser.Id, dbUser.Role);
    }
}