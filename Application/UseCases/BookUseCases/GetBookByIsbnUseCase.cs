using System.Net;
using Application.DependencyInjectionExtensions;
using Application.Dtos;
using Application.Exceptions;
using Domain.Abstractions;
using Mapster;

namespace Application.UseCases.BookUseCases;

[Service]
public class GetBookByIsbnUseCase(IBookRepository bookRepository)
{
    public async Task<BookDto> InvokeAsync(string isbn)
    {
        var result = await bookRepository.GetByIsbnAsync(isbn);

        if (result is null)
        {
            throw new LibraryApplicationException(HttpStatusCode.NotFound, "There is no book with given isbn");
        }

        return result.Adapt<BookDto>();
    }
}