using System.Net;
using Application.DependencyInjectionExtensions;
using Application.Dtos;
using Application.Exceptions;
using Application.Requests.Implementations.BookRequests;
using Domain.Abstractions;
using Mapster;

namespace Application.UseCases.BookUseCases;

[Service]
public class GetBookByIsbnUseCase(IBookRepository bookRepository)
{
    public async Task<BookDto> InvokeAsync(GetBookByIsbnRequest request)
    {
        var result = await bookRepository.GetByIsbnAsync(request.Isbn);

        if (result is null)
        {
            throw new LibraryApplicationException(ExceptionCode.EntityDoesNotExists, "There is no book with given isbn");
        }

        return result.Adapt<BookDto>();
    }
}