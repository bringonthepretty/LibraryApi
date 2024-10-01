using System.Net;
using Application.Dtos;
using Application.Exceptions;
using Domain.Abstractions;
using Microsoft.Extensions.Configuration;

namespace Application.UseCases.UserUseCases;

public class LoginUserUseCase(IUserRepository userRepository, ISecurity security, IConfiguration configuration)
{
    public async Task<TokenDto> InvokeAsync(string login, string password)
    {
        var dbUser = await userRepository.GetByLoginAsync(login);
        
        if (dbUser is null)
        {
            throw new LibraryApplicationException(HttpStatusCode.Unauthorized, "Wrong login or password");
        }
        
        if (!security.CheckIsPasswordCorrect(dbUser, password))
        {
            throw new LibraryApplicationException(HttpStatusCode.Unauthorized, "Wrong login or password");
        }

        var encryptedAccessToken = security.GenerateEncryptedAccessToken(dbUser, configuration["JwtSecurityKey"]!);
        var refreshToken = security.GenerateRefreshToken();
        
        await userRepository.UpdateRefreshTokenByIdAsync(dbUser.Id, refreshToken);
        await userRepository.SaveChangesAsync();
        
        return new TokenDto(encryptedAccessToken, refreshToken, dbUser.Id, dbUser.Role);
    }
}