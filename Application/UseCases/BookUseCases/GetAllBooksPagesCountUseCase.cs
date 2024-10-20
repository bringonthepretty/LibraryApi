using Application.DependencyInjectionExtensions;
using Application.Dtos;
using Application.Dtos.FilterMode;
using Application.Requests.Implementations.BookRequests;
using Domain.Abstractions;

namespace Application.UseCases.BookUseCases;

[Service]
public class GetAllBooksPagesCountUseCase(IBookRepository bookRepository)
{
    public async Task<int> InvokeAsync(GetAllBooksPageCountRequest request)
    {
        return request.FilterMode switch
        {
            ByName byName => (await bookRepository.CountAllWithNamePartAsync(byName.Name) + request.Limit - 1) /
                             request.Limit,
            ByGenre byGenre => (await bookRepository.CountAllWithGenreAsync(byGenre.Genre) + request.Limit - 1) /
                               request.Limit,
            ByAuthorId byAuthorId => (await bookRepository.CountAllWithAuthorIdAsync(byAuthorId.AuthorId) +
                request.Limit - 1) / request.Limit,
            ByUserId byUserId => (await bookRepository.CountAllWithUserIdAsync(byUserId.UserId) + request.Limit - 1) /
                                 request.Limit,
            _ => (await bookRepository.CountAsync() + request.Limit - 1) / request.Limit
        };
    }
}