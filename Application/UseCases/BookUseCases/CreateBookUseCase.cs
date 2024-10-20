using Application.DependencyInjectionExtensions;
using Application.Dtos;
using Application.Requests.Implementations.BookRequests;
using Domain.Abstractions;
using Domain.Entities;
using Mapster;

namespace Application.UseCases.BookUseCases;

[Service]
public class CreateBookUseCase(IBookRepository bookRepository)
{
    public async Task<BookDto> InvokeAsync(CreateBookRequest request)
    {
        var bookDbEntity = request.Adapt<Book>();
        bookDbEntity.Available = true;
        var createdBook = bookRepository.Create(bookDbEntity);
        await bookRepository.SaveChangesAsync();
        return createdBook.Adapt<BookDto>();
    } 
}