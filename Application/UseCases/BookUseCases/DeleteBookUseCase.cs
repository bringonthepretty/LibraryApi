using Domain.Abstractions;

namespace Application.UseCases.BookUseCases;

public class DeleteBookUseCase(IBookRepository bookRepository)
{
    public async Task<bool> InvokeAsync(Guid id)
    {
        var result = await bookRepository.DeleteAsync(id);
        await bookRepository.SaveChangesAsync();
        return result;
    }
}