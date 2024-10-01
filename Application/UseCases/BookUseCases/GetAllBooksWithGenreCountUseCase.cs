using Domain.Abstractions;

namespace Application.UseCases.BookUseCases;

public class GetAllBooksWithGenreCountUseCase(IBookRepository bookRepository)
{
    public async Task<int> InvokeAsync(string genre)
    {
        return await bookRepository.CountAllWithGenreAsync(genre);
    }
}