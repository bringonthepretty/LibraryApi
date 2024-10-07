using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public abstract class BaseRepository<TEntity>(LibraryDbContext dbContext): IBaseRepository<TEntity> where TEntity: Entity
{
    protected readonly DbSet<TEntity> DbSet = dbContext.Set<TEntity>();

    public TEntity Create(TEntity entity)
    {
        var result = DbSet.Add(entity);
        return result.Entity;
    }

    public TEntity Update(TEntity entity)
    {
        DbSet.Update(entity);
        return entity;
    }

    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await DbSet.FirstOrDefaultAsync(entity => entity.Id == id);
    }

    public bool Delete(TEntity entity)
    {
        DbSet.Remove(entity);
        return true;
    }

    public async Task<bool> IsExistsWithIdAsync(Guid id)
    {
        return await DbSet.AnyAsync(entity => entity.Id == id);
    }

    public async Task<int> CountAsync()
    {
        return await DbSet.CountAsync();
    }
    
    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}