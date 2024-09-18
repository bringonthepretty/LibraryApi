using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AuthorRepository(LibraryDbContext dbContext) : IAuthorRepository
{
    public async Task<Author> CreateAsync(Author author)
    {
        var result = dbContext.Authors.Add(author);
        await dbContext.SaveChangesAsync();
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
        var dbAuthor = await GetByIdAsync(author.Id);

        if (dbAuthor is null)
        {
            return await CreateAsync(author);
        }
        
        dbContext.Authors.Entry(dbAuthor).CurrentValues.SetValues(author);
        await dbContext.SaveChangesAsync();
        return author;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var result = await dbContext.Authors.Where(author => author.Id == id).ExecuteDeleteAsync();
        return result > 0;
    }

    public async Task<int> CountAsync()
    {
        return await dbContext.Authors.CountAsync();
    }
}