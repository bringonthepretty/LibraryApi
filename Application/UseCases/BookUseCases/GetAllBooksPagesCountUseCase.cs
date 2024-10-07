using Application.DependencyInjectionExtensions;
using Application.Dtos;
using Application.Dtos.FilterMode;
using Domain.Abstractions;

namespace Application.UseCases.BookUseCases;

[Service]
public class GetAllBooksPagesCountUseCase(IBookRepository bookRepository)
{
    public async Task<int> InvokeAsync(GetAllBooksPageCountCriteriaDto criteria)
    {
        return criteria.FilterMode switch
        {
            ByName byName => (await bookRepository.CountAllWithNamePartAsync(byName.Name) + criteria.Limit - 1) /
                             criteria.Limit,
            ByGenre byGenre => (await bookRepository.CountAllWithGenreAsync(byGenre.Genre) + criteria.Limit - 1) /
                               criteria.Limit,
            ByAuthorId byAuthorId => (await bookRepository.CountAllWithAuthorIdAsync(byAuthorId.AuthorId) +
                criteria.Limit - 1) / criteria.Limit,
            ByUserId byUserId => (await bookRepository.CountAllWithUserIdAsync(byUserId.UserId) + criteria.Limit - 1) /
                                 criteria.Limit,
            _ => (await bookRepository.CountAsync() + criteria.Limit - 1) / criteria.Limit
        };
    }
}