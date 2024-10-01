using Domain.Abstractions;

namespace Application.UseCases.BookUseCases;

public class DeleteBookByAuthorIdUseCase(IBookRepository bookRepository)
{
    public async Task<int> InvokeAsync(Guid authorId)
    {
        var result = await bookRepository.DeleteByAuthorIdAsync(authorId);
        await bookRepository.SaveChangesAsync();
        return result;
    }
}