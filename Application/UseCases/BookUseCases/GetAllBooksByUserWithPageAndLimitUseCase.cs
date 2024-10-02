using Application.DependencyInjectionExtensions;
using Application.Dtos;
using Domain.Abstractions;
using Mapster;

namespace Application.UseCases.BookUseCases;

[Service]
public class GetAllBooksByUserWithPageAndLimitUseCase(IBookRepository bookRepository)
{
    public async Task<List<BookDto>> InvokeAsync(Guid userId, int page, int limit)
    {
        var offset = (page - 1) * limit;
        
        return (await bookRepository.GetAllByUserIdWithOffsetAndLimitAsync(userId, offset, limit)).Select(book => book.Adapt<BookDto>()).ToList();;
    }
}