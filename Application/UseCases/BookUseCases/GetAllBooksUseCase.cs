using Application.DependencyInjectionExtensions;
using Application.Dtos;
using Application.Dtos.FilterMode;
using Application.Requests.Implementations.BookRequests;
using Domain.Abstractions;
using Mapster;

namespace Application.UseCases.BookUseCases;

[Service]
public class GetAllBooksUseCase(IBookRepository bookRepository)
{
    public async Task<List<BookDto>> InvokeAsync(GetAllBooksRequest request)
    {
        var offset = 
            (request.Page - 1) * request.Limit;

        return request.FilterMode switch
        {
            ByName byName => (await bookRepository.GetAllByNamePartWithOffsetAndLimitAsync(byName.Name, offset,
                    request.Limit)).Select(book => book.Adapt<BookDto>())
                .ToList(),
            ByGenre byGenre => (await bookRepository.GetAllByGenreWithOffsetAndLimitAsync(byGenre.Genre, offset,
                    request.Limit)).Select(book => book.Adapt<BookDto>())
                .ToList(),
            ByAuthorId byAuthorId => (await bookRepository.GetAllByAuthorIdWithOffsetAndLimitAsync(byAuthorId.AuthorId,
                    offset, request.Limit)).Select(book => book.Adapt<BookDto>())
                .ToList(),
            ByUserId byUserId => (await bookRepository.GetAllByUserIdWithOffsetAndLimitAsync(byUserId.UserId, offset,
                    request.Limit)).Select(book => book.Adapt<BookDto>())
                .ToList(),
            _ => (await bookRepository.GetAllWithOffsetAndLimitAsync(offset, request.Limit))
                .Select(book => book.Adapt<BookDto>())
                .ToList()
        };
    }
}