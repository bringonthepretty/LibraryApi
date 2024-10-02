using System.Net;
using Application.DependencyInjectionExtensions;
using Application.Dtos;
using Application.Exceptions;
using Domain.Abstractions;
using Mapster;

namespace Application.UseCases.UserUseCases;

[Service]
public class GetUserByLoginUseCase(IUserRepository userRepository)
{
    public async Task<UserDto> InvokeAsync(string login)
    {
        var result = await userRepository.GetByLoginAsync(login);

        if (result is null)
        {
            throw new LibraryApplicationException(HttpStatusCode.NotFound, "There is no user with given login");
        }

        return result.Adapt<UserDto>();
    }
}