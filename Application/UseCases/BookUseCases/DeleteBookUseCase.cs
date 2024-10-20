using Application.DependencyInjectionExtensions;
using Application.Exceptions;
using Domain.Abstractions;

namespace Application.UseCases.BookUseCases;

[Service]
public class DeleteBookUseCase(IBookRepository bookRepository)
{
    public async Task<bool> InvokeAsync(Guid id)
    {
        var bookToDelete = await bookRepository.GetByIdAsync(id);

        if (bookToDelete is null)
        {
            throw new LibraryApplicationException(ExceptionCode.EntityDoesNotExists, "Book does not exists");
        }
        
        var result = bookRepository.Delete(bookToDelete);
        await bookRepository.SaveChangesAsync();
        return result;
    }
}