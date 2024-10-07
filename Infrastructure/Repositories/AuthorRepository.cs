using Application.DependencyInjectionExtensions;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

[Service]
public class AuthorRepository(LibraryDbContext dbContext): BaseRepository<Author>(dbContext), IAuthorRepository
{

    public async Task<List<Author>> GetAllAsync()
    {
        return await DbSet.ToListAsync();
    }

    public async Task<List<Author>> GetAllWithOffsetAndLimitAsync(int offset, int limit)
    {
        return await DbSet.OrderBy(author => author.Id).Skip(offset).Take(limit).ToListAsync();
    }
}