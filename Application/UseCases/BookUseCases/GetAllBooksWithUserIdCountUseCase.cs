using Domain.Abstractions;

namespace Application.UseCases.BookUseCases;

public class GetAllBooksWithUserIdCountUseCase(IBookRepository bookRepository)
{
    public async Task<int> InvokeAsync(Guid userId)
    {
        return await bookRepository.CountAllWithUserIdAsync(userId);
    }
}