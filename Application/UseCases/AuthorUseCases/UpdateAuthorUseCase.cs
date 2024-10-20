using Application.DependencyInjectionExtensions;
using Application.Dtos;
using Application.Exceptions;
using Application.Requests.Implementations.AuthorRequests;
using Domain.Abstractions;
using Domain.Entities;
using Mapster;

namespace Application.UseCases.AuthorUseCases;


[Service]
public class UpdateAuthorUseCase(IAuthorRepository authorRepository)
{
    public async Task<AuthorDto> InvokeAsync(UpdateAuthorRequest request)
    {
        if (await authorRepository.IsExistsWithIdAsync(request.Id))
        {
            throw new LibraryApplicationException(ExceptionCode.EntityDoesNotExists, "Author does not exists");
        }
        var dbAuthor = request.Adapt<Author>();
        var updatedAuthor = authorRepository.Update(dbAuthor);
        await authorRepository.SaveChangesAsync();
        return updatedAuthor.Adapt<AuthorDto>();
    }
}