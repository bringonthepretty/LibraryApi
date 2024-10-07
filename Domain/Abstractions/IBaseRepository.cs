using Domain.Entities;

namespace Domain.Abstractions;

public interface IBaseRepository<TEntity>: IUnitOfWork where TEntity: Entity
{
    public TEntity Create(TEntity entity);
    public TEntity Update(TEntity entity);
    public Task<TEntity?> GetByIdAsync(Guid id);
    public bool Delete(TEntity entity);
    public Task<bool> IsExistsWithIdAsync(Guid id);
    public Task<int> CountAsync();
}