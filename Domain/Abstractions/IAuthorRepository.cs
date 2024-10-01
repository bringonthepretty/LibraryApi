using Domain.Entities;

namespace Domain.Abstractions;

public interface IAuthorRepository: IUnitOfWork
{
    public Task<Author> CreateAsync(Author author);
    public Task<Author?> GetByIdAsync(Guid id);
    public Task<List<Author>> GetAllAsync();
    public Task<List<Author>> GetAllWithOffsetAndLimitAsync(int offset, int limit);
    public Task<Author> UpdateAsync(Author author);
    public Task<bool> DeleteAsync(Guid id);
    public Task<int> CountAsync();
}