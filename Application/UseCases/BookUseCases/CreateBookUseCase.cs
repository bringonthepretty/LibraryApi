using Application.DependencyInjectionExtensions;
using Application.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using Mapster;

namespace Application.UseCases.BookUseCases;

[Service]
public class CreateBookUseCase(IBookRepository bookRepository)
{
    public async Task<BookDto> InvokeAsync(BookDto book)
    {
        book.Available = true;
        var bookDbEntity = book.Adapt<Book>();
        var createdBook = bookRepository.Create(bookDbEntity);
        await bookRepository.SaveChangesAsync();
        return createdBook.Adapt<BookDto>();
    } 
}