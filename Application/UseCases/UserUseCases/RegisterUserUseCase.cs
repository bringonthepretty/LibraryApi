using System.Net;
using Application.Dtos;
using Application.Exceptions;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.UseCases.UserUseCases;

public class RegisterUserUseCase(IUserRepository userRepository, ISecurity security)
{
    public async Task<bool> InvokeAsync(RegisterRequestDto registerRequest)
    {
        var dbUser = await userRepository.GetByLoginAsync(registerRequest.Login);
        
        if (dbUser is not null)
        {
            throw new LibraryApplicationException(HttpStatusCode.UnprocessableContent, "Account with this login already exist");
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

        await userRepository.CreateAsync(user);
        await userRepository.SaveChangesAsync();
        
        return true;
    }
}