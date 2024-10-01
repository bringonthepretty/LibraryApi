using Application.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using Mapster;

namespace Application.UseCases.AuthorUseCases;

public class UpdateAuthorUseCase(IAuthorRepository authorRepository)
{
    public async Task<AuthorDto> InvokeAsync(AuthorDto author)
    {
        var dbAuthor = author.Adapt<Author>();
        var updatedAuthor = await authorRepository.UpdateAsync(dbAuthor);
        await authorRepository.SaveChangesAsync();
        return updatedAuthor.Adapt<AuthorDto>();
    }
}