using Domain.Entities;

namespace Domain.Abstractions;

public interface IUserRepository: IBaseRepository<User>
{
    public Task<User?> GetByLoginAsync(string login);
    public Task<User?> GetByRefreshTokenAsync(string refreshToken);
    public Task<List<User>> GetAllAsync();
    public Task<bool> UpdateRefreshTokenByIdAsync(Guid id, string refreshToken);
}