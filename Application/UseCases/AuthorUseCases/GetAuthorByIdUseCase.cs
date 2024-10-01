using System.Net;
using Application.Dtos;
using Application.Exceptions;
using Domain.Abstractions;
using Mapster;

namespace Application.UseCases.AuthorUseCases;

public class GetAuthorByIdUseCase(IAuthorRepository authorRepository)
{
    public async Task<AuthorDto> InvokeAsync(Guid id)
    {
        var result = await authorRepository.GetByIdAsync(id);

        if (result is null)
        {
            throw new LibraryApplicationException(HttpStatusCode.NotFound, "There is no author with given id");
        }

        return result.Adapt<AuthorDto>();
    }
}