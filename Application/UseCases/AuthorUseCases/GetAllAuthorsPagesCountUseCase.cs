using Application.DependencyInjectionExtensions;
using Application.Requests.Implementations;
using Application.Requests.Implementations.AuthorRequests;
using Domain.Abstractions;

namespace Application.UseCases.AuthorUseCases;

[Service]
public class GetAllAuthorsPagesCountUseCase(IAuthorRepository authorRepository)
{
    public async Task<int> InvokeAsync(GetAllAuthorsPagesCountRequest request)
    {
        var count = await authorRepository.CountAsync();
        return (count + request.EntriesOnPage - 1) / request.EntriesOnPage;
    }
}