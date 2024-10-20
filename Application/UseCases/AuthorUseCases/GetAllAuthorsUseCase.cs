using Application.DependencyInjectionExtensions;
using Application.Dtos;
using Application.Requests.Implementations;
using Application.Requests.Implementations.AuthorRequests;
using Domain.Abstractions;
using Mapster;

namespace Application.UseCases.AuthorUseCases;

[Service]
public class GetAllAuthorsUseCase(IAuthorRepository authorRepository)
{
    public async Task<List<AuthorDto>> InvokeAsync(GetAllAuthorsRequest request)
    {
        var offset = (request.Page - 1) * request.Limit;

        return (await authorRepository.GetAllWithOffsetAndLimitAsync(offset, request.Limit)).Select(author => author.Adapt<AuthorDto>()).ToList();
    }
}