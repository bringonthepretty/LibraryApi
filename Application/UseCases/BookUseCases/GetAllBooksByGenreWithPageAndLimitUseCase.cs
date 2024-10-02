using Application.DependencyInjectionExtensions;
using Application.Dtos;
using Domain.Abstractions;
using Mapster;

namespace Application.UseCases.BookUseCases;

[Service]
public class GetAllBooksByGenreWithPageAndLimitUseCase(IBookRepository bookRepository)
{
    public async Task<List<BookDto>> InvokeAsync(string genre, int page, int limit)
    {
        var offset = (page - 1) * limit;

        return (await bookRepository.GetAllByGenreWithOffsetAndLimitAsync(genre, offset, limit)).Select(book => book.Adapt<BookDto>()).ToList();;
    }
}