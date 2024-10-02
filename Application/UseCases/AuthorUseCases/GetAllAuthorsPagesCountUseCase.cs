using Application.DependencyInjectionExtensions;
using Domain.Abstractions;

namespace Application.UseCases.AuthorUseCases;

[Service]
public class GetAllAuthorsPagesCountUseCase(IAuthorRepository authorRepository)
{
    public async Task<int> InvokeAsync(int entriesOnPage)
    {
        var count = await authorRepository.CountAsync();
        return (count + entriesOnPage - 1) / entriesOnPage;
    }
}