using Application.DependencyInjectionExtensions;
using Domain.Abstractions;

namespace Application.UseCases.BookUseCases;

[Service]
public class GetAllBooksWithAuthorIdPagesCountUseCase(IBookRepository bookRepository)
{
    public async Task<int> InvokeAsync(Guid authorId, int entriesOnPage)
    {
        var count = await bookRepository.CountAllWithAuthorIdAsync(authorId);
        return (count + entriesOnPage - 1) / entriesOnPage;
    }
}