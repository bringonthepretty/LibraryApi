using Application.DependencyInjectionExtensions;
using Application.Dtos;
using Application.Requests.Implementations.AuthorRequests;
using Domain.Abstractions;
using Domain.Entities;
using Mapster;

namespace Application.UseCases.AuthorUseCases;

[Service]
public class CreateAuthorUseCase(IAuthorRepository authorRepository)
{
    public async Task<AuthorDto> InvokeAsync(CreateAuthorRequest request)
    {
        var authorDbEntity = request.Adapt<Author>();
        var createdAuthor = authorRepository.Create(authorDbEntity);
        await authorRepository.SaveChangesAsync();
        return createdAuthor.Adapt<AuthorDto>();
    }
}