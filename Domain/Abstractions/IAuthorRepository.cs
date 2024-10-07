using Domain.Entities;

namespace Domain.Abstractions;

public interface IAuthorRepository: IBaseRepository<Author>
{
    public Task<List<Author>> GetAllAsync();
    public Task<List<Author>> GetAllWithOffsetAndLimitAsync(int offset, int limit);
}