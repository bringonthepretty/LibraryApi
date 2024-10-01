using Application.Dtos;
using Application.Exceptions;
using Application.UseCases.UserUseCases;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Tests.Application.ServiceTests.AuthServiceTests;

public class RegisterTests
{
    
    [Fact]
    public async Task RegisterTest_Positive_ReturnTrue()
    {
        var userRepositoryMock = new Mock<IUserRepository>();
        var configurationMock = new Mock<IConfiguration>();
        var securityMock = new Mock<ISecurity>();
        var registerRequestDto = new RegisterRequestDto()
        {
            Login = "login",
            Password = "password",
            Username = "password",
        };
        User? user = null;
        userRepositoryMock.Setup(repository => repository.GetByLoginAsync("")).ReturnsAsync(user);

        var authService = new RegisterUserUseCase(userRepositoryMock.Object, securityMock.Object);
        var result = await authService.InvokeAsync(registerRequestDto);
        Assert.True(result);

    }
    
    [Fact]
    public async Task RegisterTest_LoginAlreadyExists_Throws()
    {
        var userRepositoryMock = new Mock<IUserRepository>();
        var configurationMock = new Mock<IConfiguration>();
        var securityMock = new Mock<ISecurity>();
        var registerRequestDto = new RegisterRequestDto()
        {
            Login = "login",
            Password = "password",
            Username = "password",
        };
        var user = new User();
        userRepositoryMock.Setup(repository => repository.GetByLoginAsync(registerRequestDto.Login)).ReturnsAsync(user);

        var authService = new RegisterUserUseCase(userRepositoryMock.Object, securityMock.Object);
        await Assert.ThrowsAsync<LibraryApplicationException>(async () => await authService.InvokeAsync(registerRequestDto));
    }
}