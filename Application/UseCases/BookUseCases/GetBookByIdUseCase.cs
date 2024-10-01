using System.Net;
using Application.Dtos;
using Application.Exceptions;
using Domain.Abstractions;
using Mapster;

namespace Application.UseCases.BookUseCases;

public class GetBookByIdUseCase(IBookRepository bookRepository)
{
    public async Task<BookDto> InvokeAsync(Guid id)
    {
        var result = await bookRepository.GetByIdAsync(id);

        if (result is null)
        {
            throw new LibraryApplicationException(HttpStatusCode.NotFound, "There is no book with given id");
        }

        return result.Adapt<BookDto>();
    }
}