using Application.DependencyInjectionExtensions;
using Domain.Abstractions;

namespace Application.UseCases.BookUseCases;

[Service]
public class GetAllBooksWithUserIdCountUseCase(IBookRepository bookRepository)
{
    public async Task<int> InvokeAsync(Guid userId)
    {
        return await bookRepository.CountAllWithUserIdAsync(userId);
    }
}