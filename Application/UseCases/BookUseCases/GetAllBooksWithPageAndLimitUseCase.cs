using Application.DependencyInjectionExtensions;
using Application.Dtos;
using Domain.Abstractions;
using Mapster;

namespace Application.UseCases.BookUseCases;

[Service]
public class GetAllBooksWithPageAndLimitUseCase(IBookRepository bookRepository)
{
    public async Task<List<BookDto>> InvokeAsync(int page, int limit)
    {
        var offset = (page - 1) * limit;

        return (await bookRepository.GetAllWithOffsetAndLimitAsync(offset, limit)).Select(book => book.Adapt<BookDto>()).ToList();
    }
}