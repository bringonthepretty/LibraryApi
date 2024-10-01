using Domain.Abstractions;

namespace Application.UseCases.BookUseCases;

public class GetAllBooksWithNamePagesCountUseCase(IBookRepository bookRepository)
{
    public async Task<int> InvokeAsync(string name, int entriesOnPage)
    {
        var count = await bookRepository.CountAllWithNamePartAsync(name);
        return (count + entriesOnPage - 1) / entriesOnPage;
    }
}