using System.Net;
using Application.Dtos;
using Application.Exceptions;
using Domain.Abstractions;
using Mapster;

namespace Application.UseCases.UserUseCases;

public class GetUserByIdUseCase(IUserRepository userRepository)
{
    public async Task<UserDto> InvokeAsync(Guid id)
    {
        var result = await userRepository.GetByIdAsync(id);

        if (result is null)
        {
            throw new LibraryApplicationException(HttpStatusCode.NotFound, "There is no user with given id");
        }

        return result.Adapt<UserDto>();
    }
}