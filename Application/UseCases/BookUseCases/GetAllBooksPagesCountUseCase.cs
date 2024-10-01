using Domain.Abstractions;

namespace Application.UseCases.BookUseCases;

public class GetAllBooksPagesCountUseCase(IBookRepository bookRepository)
{
    public async Task<int> InvokeAsync(int entriesOnPage)
    {
        var count = await bookRepository.CountAllAsync();
        return (count + entriesOnPage - 1) / entriesOnPage;
    }
}