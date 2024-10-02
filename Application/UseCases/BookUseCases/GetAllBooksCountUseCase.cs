using Application.DependencyInjectionExtensions;
using Domain.Abstractions;

namespace Application.UseCases.BookUseCases;

[Service]
public class GetAllBooksCountUseCase(IBookRepository bookRepository)
{
    public async Task<int> InvokeAsync()
    {
        return await bookRepository.CountAllAsync();
    }
}