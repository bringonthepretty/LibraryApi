using Application.Dtos;
using Application.Dtos.FilterMode;
using Domain.Abstractions;
using Mapster;

namespace Application.UseCases.BookUseCases;

public class GetAllBooksUseCase(IBookRepository bookRepository)
{
    public async Task<List<BookDto>> InvokeAsync(GetAllBooksCriteriaDto criteria)
    {
        var offset = 
            (criteria.Page - 1) * criteria.Limit;

        return criteria.FilterMode switch
        {
            ByName byName => (await bookRepository.GetAllByNamePartWithOffsetAndLimitAsync(byName.Name, offset,
                    criteria.Limit)).Select(book => book.Adapt<BookDto>())
                .ToList(),
            ByGenre byGenre => (await bookRepository.GetAllByGenreWithOffsetAndLimitAsync(byGenre.Genre, offset,
                    criteria.Limit)).Select(book => book.Adapt<BookDto>())
                .ToList(),
            ByAuthorId byAuthorId => (await bookRepository.GetAllByAuthorIdWithOffsetAndLimitAsync(byAuthorId.AuthorId,
                    offset, criteria.Limit)).Select(book => book.Adapt<BookDto>())
                .ToList(),
            ByUserId byUserId => (await bookRepository.GetAllByUserIdWithOffsetAndLimitAsync(byUserId.UserId, offset,
                    criteria.Limit)).Select(book => book.Adapt<BookDto>())
                .ToList(),
            _ => (await bookRepository.GetAllWithOffsetAndLimitAsync(offset, criteria.Limit))
                .Select(book => book.Adapt<BookDto>())
                .ToList()
        };
    }
}