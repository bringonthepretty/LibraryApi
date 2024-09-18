using Application.Dtos;

namespace Application.Services.Api;

public interface IBookService
{
    public Task<BookDto> CreateAsync(BookDto book);
    public Task<BookDto> GetByIdAsync(Guid id);
    public Task<BookDto> GetByIsbnAsync(string isbn);
    public Task<List<BookDto>> GetAllWithPageAndLimitAsync(string page, string limit);

    public Task<List<BookDto>> GetAllByNameWithPageAndLimitAsync(string name, string page, string limit);
    public Task<List<BookDto>> GetAllByGenreWithPageAndLimitAsync(string genre, string page, string limit);
    public Task<List<BookDto>> GetAllByAuthorWithPageAndLimitAsync(Guid authorId, string page, string limit);
    public Task<List<BookDto>> GetAllByUserWithPageAndLimitAsync(Guid userId, string page, string limit);
    public Task<BookDto> UpdateAsync(BookDto book);
    public Task<bool> DeleteAsync(Guid id);
    public Task<int> DeleteByAuthorIdAsync(Guid authorId);
    public Task<bool> BorrowBookAsync(Guid bookId);
    public Task<bool> ReturnBookAsync(Guid bookId);

    public Task<int> GetAllBooksCountAsync();
    public Task<int> GetAllBooksPagesCountAsync(string entriesOnPage);
    public Task<int> GetAllBooksWithNameCountAsync(string name);
    public Task<int> GetAllBooksWithNamePagesCountAsync(string name, string entriesOnPage);
    public Task<int> GetAllBooksWithGenreCountAsync(string genre);
    public Task<int> GetAllBooksWithGenrePagesCountAsync(string genre, string entriesOnPage);
    public Task<int> GetAllBooksWithAuthorIdCountAsync(Guid authorId);
    public Task<int> GetAllBooksWithAuthorIdPagesCountAsync(Guid authorId, string entriesOnPage);
    public Task<int> GetAllBooksWithUserIdCountAsync(Guid userId);
    public Task<int> GetAllBooksWithUserIdPagesCountAsync(Guid userId, string entriesOnPage);
}