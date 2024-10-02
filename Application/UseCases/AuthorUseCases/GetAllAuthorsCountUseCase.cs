using Application.DependencyInjectionExtensions;
using Domain.Abstractions;

namespace Application.UseCases.AuthorUseCases;

[Service]
public class GetAllAuthorsCountUseCase(IAuthorRepository authorRepository)
{
    public async Task<int> InvokeAsync()
    {
        return await authorRepository.CountAsync();
    }
}