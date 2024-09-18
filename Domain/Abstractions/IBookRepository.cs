using Domain.Entities;

namespace Domain.Abstractions;

public interface IBookRepository
{
    public Task<Book> CreateAsync(Book book);
    public Task<Book?> GetByIdAsync(Guid id);
    public Task<Book?> GetByIsbnAsync(string isbn);
    public Task<Book?> GetByExactNameAsync(string name);
    public Task<List<Book>> GetAllAsync();
    public Task<List<Book>> GetAllByAuthorIdAsync(Guid authorId);
    public Task<List<Book>> GetAllByNamePartAsync(string namePart);
    public Task<List<Book>> GetAllByGenreAsync(string genre);

    public Task<List<Book>> GetAllWithOffsetAndLimitAsync(int offset, int limit);
    
    public Task<List<Book>> GetAllByAuthorIdWithOffsetAndLimitAsync(Guid authorId, int offset, int limit);
    public Task<List<Book>> GetAllByUserIdWithOffsetAndLimitAsync(Guid userId, int offset, int limit);
    public Task<List<Book>> GetAllByNamePartWithOffsetAndLimitAsync(string namePart, int offset, int limit);
    public Task<List<Book>> GetAllByGenreWithOffsetAndLimitAsync(string genre, int offset, int limit);
    public Task<Book> UpdateAsync(Book book);
    public Task<bool> DeleteAsync(Guid id);

    public Task<int> DeleteByAuthorIdAsync(Guid authorId);

    public Task<int> CountAllAsync();
    public Task<int> CountAllWithNamePartAsync(string namePart);
    public Task<int> CountAllWithAuthorIdAsync(Guid authorId);
    public Task<int> CountAllWithUserIdAsync(Guid userId);
    public Task<int> CountAllWithGenreAsync(string genre);
}