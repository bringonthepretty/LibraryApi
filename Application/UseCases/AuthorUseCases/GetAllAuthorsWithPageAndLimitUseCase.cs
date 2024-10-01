using Application.Dtos;
using Domain.Abstractions;
using Mapster;

namespace Application.UseCases.AuthorUseCases;

public class GetAllAuthorsWithPageAndLimitUseCase(IAuthorRepository authorRepository)
{
    public async Task<List<AuthorDto>> InvokeAsync(int page, int limit)
    {
        var offset = (page - 1) * limit;

        return (await authorRepository.GetAllWithOffsetAndLimitAsync(offset, limit)).Select(author => author.Adapt<AuthorDto>()).ToList();
    }
}