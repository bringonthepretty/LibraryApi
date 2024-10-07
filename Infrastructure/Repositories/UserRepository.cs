using Application.DependencyInjectionExtensions;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

[Service]
public class UserRepository(LibraryDbContext dbContext): BaseRepository<User>(dbContext), IUserRepository
{
    public async Task<User?> GetByLoginAsync(string login)
    {
        return await DbSet.FirstOrDefaultAsync(user => user.Login == login);
    }

    public async Task<User?> GetByRefreshTokenAsync(string refreshToken)
    {
        return await DbSet.FirstOrDefaultAsync(user => user.RefreshToken == refreshToken);
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await DbSet.ToListAsync();
    }

    public async Task<bool> UpdateRefreshTokenByIdAsync(Guid id, string refreshToken)
    {
        var user = await GetByIdAsync(id);
        user.RefreshToken = refreshToken;
        return true;
    }
}