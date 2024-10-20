using Application.DependencyInjectionExtensions;
using Application.Exceptions;
using Application.Requests.Implementations.BookRequests;
using Domain.Abstractions;

namespace Application.UseCases.BookUseCases;

[Service]
public class DeleteBookUseCase(IBookRepository bookRepository)
{
    public async Task<bool> InvokeAsync(DeleteBookRequest request)
    {
        var bookToDelete = await bookRepository.GetByIdAsync(request.Id);

        if (bookToDelete is null)
        {
            throw new LibraryApplicationException(ExceptionCode.EntityDoesNotExists, "Book does not exists");
        }
        
        var result = bookRepository.Delete(bookToDelete);
        await bookRepository.SaveChangesAsync();
        return result;
    }
}