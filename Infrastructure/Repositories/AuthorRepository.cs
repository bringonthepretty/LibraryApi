using Application.DependencyInjectionExtensions;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

[Service]
public class AuthorRepository(LibraryDbContext dbContext) : IAuthorRepository
{
    public async Task<Author> CreateAsync(Author author)
    {
        var result = dbContext.Authors.Add(author);
        return result.Entity;
    }

    public async Task<Author?> GetByIdAsync(Guid id)
    {
        return await dbContext.Authors.FirstOrDefaultAsync(author => author.Id == id);
    }

    public async Task<List<Author>> GetAllAsync()
    {
        return await dbContext.Authors.ToListAsync();
    }

    public async Task<List<Author>> GetAllWithOffsetAndLimitAsync(int offset, int limit)
    {
        return await dbContext.Authors.OrderBy(author => author.Id).Skip(offset).Take(limit).ToListAsync();
    }

    public async Task<Author> UpdateAsync(Author author)
    {
        dbContext.Authors.Update(author);
        return author;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        dbContext.Authors.Remove(new Author { Id = id });
        return true;
    }

    public async Task<int> CountAsync()
    {
        return await dbContext.Authors.CountAsync();
    }

    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}