using Application.DependencyInjectionExtensions;
using Application.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using Mapster;

namespace Application.UseCases.BookUseCases;

[Service]
public class UpdateBookUseCase(IBookRepository bookRepository)
{
    public async Task<BookDto> InvokeAsync(BookDto book)
    {
        if (await bookRepository.IsExistsWithIdAsync(book.Id))
        {
            throw new ApplicationException();
        }
        var bookDbEntity = book.Adapt<Book>();
        var updatedBook = bookRepository.Update(bookDbEntity);
        await bookRepository.SaveChangesAsync();
        return updatedBook.Adapt<BookDto>();
    }
}