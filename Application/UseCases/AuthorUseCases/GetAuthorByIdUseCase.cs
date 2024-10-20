using System.Net;
using Application.DependencyInjectionExtensions;
using Application.Dtos;
using Application.Exceptions;
using Application.Requests.Implementations.AuthorRequests;
using Domain.Abstractions;
using Mapster;

namespace Application.UseCases.AuthorUseCases;

[Service]
public class GetAuthorByIdUseCase(IAuthorRepository authorRepository)
{
    public async Task<AuthorDto> InvokeAsync(GetAuthorByIdRequest request)
    {
        var result = await authorRepository.GetByIdAsync(request.Id);

        if (result is null)
        {
            throw new LibraryApplicationException( "There is no author with given id");
        }

        return result.Adapt<AuthorDto>();
    }
}