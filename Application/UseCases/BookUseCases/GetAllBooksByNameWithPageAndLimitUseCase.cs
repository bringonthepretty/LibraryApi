using Application.DependencyInjectionExtensions;
using Application.Dtos;
using Domain.Abstractions;
using Mapster;

namespace Application.UseCases.BookUseCases;

[Service]
public class GetAllBooksByNameWithPageAndLimitUseCase(IBookRepository bookRepository)
{
    public async Task<List<BookDto>> InvokeAsync(string name, int page, int limit)
    {

        var offset = (page - 1) * limit;

        return (await bookRepository.GetAllByNamePartWithOffsetAndLimitAsync(name, offset, limit)).Select(book => book.Adapt<BookDto>()).ToList();;
    }
}