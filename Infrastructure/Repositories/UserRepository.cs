using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository(LibraryDbContext dbContext) : IUserRepository
{
    public async Task<User> CreateAsync(User user)
    {
        var result = dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await dbContext.Users.FirstOrDefaultAsync(user => user.Id == id);
    }

    public async Task<User?> GetByLoginAsync(string login)
    {
        return await dbContext.Users.FirstOrDefaultAsync(user => user.Login == login);
    }

    public async Task<User?> GetByRefreshTokenAsync(string refreshToken)
    {
        return await dbContext.Users.FirstOrDefaultAsync(user => user.RefreshToken == refreshToken);
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await dbContext.Users.ToListAsync();
    }

    public async Task<User> UpdateAsync(User user)
    {
        var dbUser = await GetByIdAsync(user.Id);

        if (dbUser is null)
        {
            return await CreateAsync(user);
        }

        dbContext.Users.Entry(dbUser).CurrentValues.SetValues(user);
        await dbContext.SaveChangesAsync();
        return user;
    }

    public async Task<bool> UpdateRefreshTokenByIdAsync(Guid id, string refreshToken)
    {
        var user = await GetByIdAsync(id);

        if (user is null)
        {
            return false;
        }

        user.RefreshToken = refreshToken;
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var result = await dbContext.Users.Where(user => user.Id == id).ExecuteDeleteAsync();
        return result > 0;
    }
}