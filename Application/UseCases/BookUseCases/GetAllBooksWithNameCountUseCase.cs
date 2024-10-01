using Domain.Abstractions;

namespace Application.UseCases.BookUseCases;

public class GetAllBooksWithNameCountUseCase(IBookRepository bookRepository)
{
    public async Task<int> InvokeAsync(string name)
    {
        return await bookRepository.CountAllWithNamePartAsync(name);
    }
}