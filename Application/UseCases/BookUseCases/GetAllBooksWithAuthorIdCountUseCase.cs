using Application.DependencyInjectionExtensions;
using Domain.Abstractions;

namespace Application.UseCases.BookUseCases;

[Service]
public class GetAllBooksWithAuthorIdCountUseCase(IBookRepository bookRepository)
{
    public async Task<int> InvokeAsync(Guid authorId)
    {
        return await bookRepository.CountAllWithAuthorIdAsync(authorId);
    }
}