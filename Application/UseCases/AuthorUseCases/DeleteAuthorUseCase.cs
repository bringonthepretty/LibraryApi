using Application.DependencyInjectionExtensions;
using Domain.Abstractions;

namespace Application.UseCases.AuthorUseCases;

[Service]
public class DeleteAuthorUseCase(IAuthorRepository authorRepository, IBookRepository bookRepository)
{
    public async Task<bool> InvokeAsync(Guid id)
    {
        var authorToDelete = await authorRepository.GetByIdAsync(id);

        if (authorToDelete is null)
        {
            return false;
        }
        
        bookRepository.DeleteByAuthorId(id);
        var result = authorRepository.Delete(authorToDelete);
        await authorRepository.SaveChangesAsync();
        return result;
    }
}