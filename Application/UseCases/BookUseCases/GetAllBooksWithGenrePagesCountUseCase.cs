using Application.DependencyInjectionExtensions;
using Domain.Abstractions;

namespace Application.UseCases.BookUseCases;

[Service]
public class GetAllBooksWithGenrePagesCountUseCase(IBookRepository bookRepository)
{
    public async Task<int> InvokeAsync(string genre, int entriesOnPage)
    {
        var count = await bookRepository.CountAllWithGenreAsync(genre);
        return (count + entriesOnPage - 1) / entriesOnPage;
    }
}