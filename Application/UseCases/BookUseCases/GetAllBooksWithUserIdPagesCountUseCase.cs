using Application.DependencyInjectionExtensions;
using Domain.Abstractions;

namespace Application.UseCases.BookUseCases;

[Service]
public class GetAllBooksWithUserIdPagesCountUseCase(IBookRepository bookRepository)
{
    public async Task<int> InvokeAsync(Guid userId, int entriesOnPage)
    {
        var count = await bookRepository.CountAllWithUserIdAsync(userId);
        return (count + entriesOnPage - 1) / entriesOnPage;
    }
}