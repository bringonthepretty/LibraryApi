using System.Net;
using Application.DependencyInjectionExtensions;
using Application.Dtos;
using Application.Exceptions;
using Application.Requests.Implementations.UserRequests;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.UseCases.UserUseCases;

[Service]
public class RegisterUserUseCase(IUserRepository userRepository, ISecurity security)
{
    public async Task<bool> InvokeAsync(RegisterUserRequest request)
    {
        var dbUser = await userRepository.GetByLoginAsync(request.Login);
        
        if (dbUser is not null)
        {
            throw new LibraryApplicationException(ExceptionCode.ImpossibleData, "Account with this login already exist");
        }
        
        var passwordKeyAndHash = security.GeneratePasswordKeyAndHashFromString(request.Password);
        
        var user = new User
        {
            Username = request.Username,
            Login = request.Login,
            Role = "user",
            PasswordKey = passwordKeyAndHash.Key,
            PasswordHash = passwordKeyAndHash.Value
        };

        userRepository.Create(user);
        await userRepository.SaveChangesAsync();
        return true;
    }
}