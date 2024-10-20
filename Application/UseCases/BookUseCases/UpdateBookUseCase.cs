using Application.DependencyInjectionExtensions;
using Application.Dtos;
using Application.Exceptions;
using Application.Requests.Implementations.BookRequests;
using Domain.Abstractions;
using Domain.Entities;
using Mapster;

namespace Application.UseCases.BookUseCases;

[Service]
public class UpdateBookUseCase(IBookRepository bookRepository)
{
    public async Task<BookDto> InvokeAsync(UpdateBookRequest request)
    {
        if (await bookRepository.IsExistsWithIdAsync(request.Id))
        {
            throw new LibraryApplicationException(ExceptionCode.EntityDoesNotExists, "Book does not exists");
        }
        var bookDbEntity = request.Adapt<Book>();
        var updatedBook = bookRepository.Update(bookDbEntity);
        await bookRepository.SaveChangesAsync();
        return updatedBook.Adapt<BookDto>();
    }
}