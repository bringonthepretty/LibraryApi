using Application.DependencyInjectionExtensions;
using Application.Exceptions;
using Application.Requests.Implementations;
using Application.Requests.Implementations.AuthorRequests;
using Domain.Abstractions;

namespace Application.UseCases.AuthorUseCases;

[Service]
public class DeleteAuthorUseCase(IAuthorRepository authorRepository, IBookRepository bookRepository)
{
    public async Task<bool> InvokeAsync(DeleteAuthorRequest request)
    {
        var authorToDelete = await authorRepository.GetByIdAsync(request.Id);

        if (authorToDelete is null)
        {
            throw new LibraryApplicationException(ExceptionCode.EntityDoesNotExists, "Author does not exists");
        }
        
        bookRepository.DeleteByAuthorId(request.Id);
        var result = authorRepository.Delete(authorToDelete);
        await authorRepository.SaveChangesAsync();
        return result;
    }
}