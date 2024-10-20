using Application.DependencyInjectionExtensions;
using Application.Dtos;
using Application.Exceptions;
using Domain.Abstractions;
using Domain.Entities;
using Mapster;

namespace Application.UseCases.AuthorUseCases;


[Service]
public class UpdateAuthorUseCase(IAuthorRepository authorRepository)
{
    public async Task<AuthorDto> InvokeAsync(AuthorDto author)
    {
        if (await authorRepository.IsExistsWithIdAsync(author.Id))
        {
            throw new LibraryApplicationException(ExceptionCode.EntityDoesNotExists, "Author does not exists");
        }
        var dbAuthor = author.Adapt<Author>();
        var updatedAuthor = authorRepository.Update(dbAuthor);
        await authorRepository.SaveChangesAsync();
        return updatedAuthor.Adapt<AuthorDto>();
    }
}