using Application.UseCases.BookUseCases;
using Domain.Abstractions;

namespace Application.UseCases.AuthorUseCases;

public class DeleteAuthorUseCase(IAuthorRepository authorRepository, DeleteBookByAuthorIdUseCase deleteBookByAuthorIdUseCase)
{
    public async Task<bool> InvokeAsync(Guid id)
    {
        await deleteBookByAuthorIdUseCase.InvokeAsync(id);
        var result = await authorRepository.DeleteAsync(id);
        await authorRepository.SaveChangesAsync();
        return result;
    }
}