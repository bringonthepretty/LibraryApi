using System.Net;
using Application.DependencyInjectionExtensions;
using Application.Dtos;
using Application.Exceptions;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.UseCases.UserUseCases;

[Service]
public class RegisterUserUseCase(IUserRepository userRepository, ISecurity security)
{
    public async Task InvokeAsync(RegisterRequestDto registerRequest)
    {
        var dbUser = await userRepository.GetByLoginAsync(registerRequest.Login);
        
        if (dbUser is not null)
        {
            throw new LibraryApplicationException(ExceptionCode.ImpossibleData, "Account with this login already exist");
        }
        
        var passwordKeyAndHash = security.GeneratePasswordKeyAndHashFromString(registerRequest.Password);
        
        var user = new User
        {
            Username = registerRequest.Username,
            Login = registerRequest.Login,
            Role = "user",
            PasswordKey = passwordKeyAndHash.Key,
            PasswordHash = passwordKeyAndHash.Value
        };

        userRepository.Create(user);
        await userRepository.SaveChangesAsync();
    }
}