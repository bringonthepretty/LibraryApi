using Application.Dtos;

namespace Application.Services.Api;

public interface IUserService
{
    public Task<UserDto> GetByIdAsync(Guid id);
    public Task<UserDto> GetByLoginAsync(string login);
    public Task<List<BookDto>> GetUsersBooks(int page, int limit);
    public Task<int> GetAllBooksWithUserIdPagesCountAsync(int limit);
}