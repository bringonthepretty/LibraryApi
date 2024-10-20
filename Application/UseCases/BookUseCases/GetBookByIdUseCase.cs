using System.Net;
using Application.DependencyInjectionExtensions;
using Application.Dtos;
using Application.Exceptions;
using Application.Requests.Implementations.BookRequests;
using Domain.Abstractions;
using Mapster;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.BookUseCases;

[Service]
public class GetBookByIdUseCase(IBookRepository bookRepository)
{
    public async Task<BookDto> InvokeAsync(GetBookByIdRequest request)
    {
        var result = await bookRepository.GetByIdAsync(request.Id);

        if (result is null)
        {
            throw new LibraryApplicationException(ExceptionCode.EntityDoesNotExists, "There is no book with given id");
        }

        return result.Adapt<BookDto>();
    }
}