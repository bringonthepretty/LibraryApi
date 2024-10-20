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

public class LoginTests
{
    [Fact]
    public async Task LoginTest_Positive_ReturnsTokenDto()
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
        var user = new User
        {
            Id = id,
            Login = login,
            PasswordKey = passwordKey,
            PasswordHash = passwordHash,
            Role = "user"
        };

        userRepositoryMock.Setup(repository => repository.GetByLoginAsync(login)).ReturnsAsync(user);
        configurationMock.Setup(configuration => configuration["JwtSecurityKey"]).Returns("jwtsecurityleyjwtsecurityleyjwtsecurityleyjwtsecurityleyjwtsecurityleyjwtsecurityleyjwtsecurityleyjwtsecurityleyjwtsecurityleyjwtsecurityley");

        var authService = new LoginUserUseCase(userRepositoryMock.Object, securityMock.Object, configurationMock.Object);
        var result = await authService.InvokeAsync(new LoginUserRequest(login, password, null));
        Assert.Equal(id, result.Id);
    }
    
    [Fact]
    public async Task LoginTest_LoginDontExists_Throws()
    {
        var userRepositoryMock = new Mock<IUserRepository>();
        var configurationMock = new Mock<IConfiguration>();
        var securityMock = new Mock<ISecurity>();
        var login = "login";
        var password = "password";
        
        User? user = null;

        userRepositoryMock.Setup(repository => repository.GetByLoginAsync(login)).ReturnsAsync(user);
        configurationMock.Setup(configuration => configuration["JwtSecurityKey"]).Returns("jwtsecurityleyjwtsecurityleyjwtsecurityleyjwtsecurityleyjwtsecurityleyjwtsecurityleyjwtsecurityleyjwtsecurityleyjwtsecurityleyjwtsecurityley");

        var authService = new LoginUserUseCase(userRepositoryMock.Object, securityMock.Object, configurationMock.Object);
        await Assert.ThrowsAsync<LibraryApplicationException>(async () => await authService.InvokeAsync(new LoginUserRequest(login, password, null)));
    }
    
    [Fact]
    public async Task LoginTest_WrongPassword_Throws()
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
        var user = new User
        {
            Id = id,
            Login = login,
            PasswordKey = passwordKey,
            PasswordHash = passwordHash,
            Role = "user"
        };

        userRepositoryMock.Setup(repository => repository.GetByLoginAsync(login)).ReturnsAsync(user);
        configurationMock.Setup(configuration => configuration["JwtSecurityKey"]).Returns("jwtsecurityleyjwtsecurityleyjwtsecurityleyjwtsecurityleyjwtsecurityleyjwtsecurityleyjwtsecurityleyjwtsecurityleyjwtsecurityleyjwtsecurityley");

        var authService = new LoginUserUseCase(userRepositoryMock.Object, securityMock.Object, configurationMock.Object);
        await Assert.ThrowsAsync<LibraryApplicationException>(async () => await authService.InvokeAsync(new LoginUserRequest(login, password + "asdasd", null)));
    }
}