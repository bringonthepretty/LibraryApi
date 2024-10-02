using Application.DependencyInjectionExtensions;
using Application.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using Mapster;

namespace Application.UseCases.AuthorUseCases;

[Service]
public class CreateAuthorUseCase(IAuthorRepository authorRepository)
{
    public async Task<AuthorDto> InvokeAsync(AuthorDto author)
    {
        var authorDbEntity = author.Adapt<Author>();
        var createdAuthor = await authorRepository.CreateAsync(authorDbEntity);
        await authorRepository.SaveChangesAsync();
        return createdAuthor.Adapt<AuthorDto>();
    }
}