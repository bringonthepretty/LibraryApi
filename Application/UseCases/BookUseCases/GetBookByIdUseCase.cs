using System.Net;
using Application.DependencyInjectionExtensions;
using Application.Dtos;
using Application.Exceptions;
using Domain.Abstractions;
using Mapster;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.BookUseCases;

[Service]
public class GetBookByIdUseCase(IBookRepository bookRepository)
{
    public async Task<BookDto> InvokeAsync(Guid id)
    {
        var result = await bookRepository.GetByIdAsync(id);

        if (result is null)
        {
            throw new LibraryApplicationException(ExceptionCode.EntityDoesNotExists, "There is no book with given id");
        }

        return result.Adapt<BookDto>();
    }
}