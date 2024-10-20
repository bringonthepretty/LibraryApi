using System.Security.Cryptography;
using System.Text;
using Application.Exceptions;
using Application.Requests.Implementations.UserRequests;
using Application.UseCases.UserUseCases;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Tests.Application.ServiceTests.AuthServiceTests;

public class RegenerateTokensTests
{
    [Fact]
    public async Task RegenerateTokensTest_Positive()
    {
        var userRepositoryMock = new Mock<IUserRepository>();
        var configurationMock = new Mock<IConfiguration>();
        var securityMock = new Mock<ISecurity>();
        
        var login = "login";
        var password = "password";
        byte[] passwordKey;
        byte[] passwordHash;

        using (var hmac = new HMACSHA512())
        {
            passwordKey = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
        
        var id = new Guid();
        var oldRefreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        
        var user = new User
        {
            Id = id,
            Login = login,
            PasswordKey = passwordKey,
            PasswordHash = passwordHash,
            RefreshToken = oldRefreshToken,
            Role = "user"
        };

        userRepositoryMock.Setup(repository => repository.GetByRefreshTokenAsync(oldRefreshToken)).ReturnsAsync(user);
        configurationMock.Setup(configuration => configuration["JwtSecurityKey"]).Returns("jwtsecurityleyjwtsecurityleyjwtsecurityleyjwtsecurityleyjwtsecurityleyjwtsecurityleyjwtsecurityleyjwtsecurityleyjwtsecurityleyjwtsecurityley");

        var authService = new RegenerateUserAccessAndRefreshTokensUseCase(userRepositoryMock.Object, securityMock.Object, configurationMock.Object);
        
        var result = await authService.InvokeAsync(new RegenerateUserAccessAndRefreshTokensRequest(oldRefreshToken, null));
        Assert.Equal(id, result.Id);
    }
    
    [Fact]
    public async Task RegenerateTokensTest_OldRefreshTokenDontExists_Throws()
    {
        var userRepositoryMock = new Mock<IUserRepository>();
        var configurationMock = new Mock<IConfiguration>();
        var securityMock = new Mock<ISecurity>();
        var oldRefreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        
        User? user = null;

        userRepositoryMock.Setup(repository => repository.GetByRefreshTokenAsync(oldRefreshToken)).ReturnsAsync(user);
        configurationMock.Setup(configuration => configuration["JwtSecurityKey"]).Returns("jwtsecurityleyjwtsecurityleyjwtsecurityleyjwtsecurityleyjwtsecurityleyjwtsecurityleyjwtsecurityleyjwtsecurityleyjwtsecurityleyjwtsecurityley");

        var authService = new RegenerateUserAccessAndRefreshTokensUseCase(userRepositoryMock.Object, securityMock.Object, configurationMock.Object);
        
        await Assert.ThrowsAsync<LibraryApplicationException>(async () => await authService.InvokeAsync(new RegenerateUserAccessAndRefreshTokensRequest(oldRefreshToken, null)));
    }
    
    [Fact]
    public async Task RegenerateTokensTest_OldRefreshTokenNull_Throws()
    {
        var userRepositoryMock = new Mock<IUserRepository>();
        var configurationMock = new Mock<IConfiguration>();
        var securityMock = new Mock<ISecurity>();
        string? oldRefreshToken = null;

        var authService = new RegenerateUserAccessAndRefreshTokensUseCase(userRepositoryMock.Object, securityMock.Object, configurationMock.Object);
        
        await Assert.ThrowsAsync<LibraryApplicationException>(async () => await authService.InvokeAsync(new RegenerateUserAccessAndRefreshTokensRequest(oldRefreshToken, null)));
    }
}