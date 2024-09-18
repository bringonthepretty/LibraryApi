using Domain.Entities;

namespace Domain.Abstractions;

public interface IUserRepository
{
    public Task<User> CreateAsync(User user);
    public Task<User?> GetByIdAsync(Guid id);
    public Task<User?> GetByLoginAsync(string login);
    public Task<User?> GetByRefreshTokenAsync(string refreshToken);
    public Task<List<User>> GetAllAsync();
    public Task<User> UpdateAsync(User user);
    public Task<bool> UpdateRefreshTokenByIdAsync(Guid id, string refreshToken);
    public Task<bool> DeleteAsync(Guid id);

}