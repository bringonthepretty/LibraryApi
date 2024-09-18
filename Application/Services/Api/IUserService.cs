using Application.Dtos;

namespace Application.Services.Api;

public interface IUserService
{
    public Task<UserDto> GetByIdAsync(Guid id);
    public Task<UserDto> GetByLoginAsync(string login);
    public Task<List<BookDto>> GetUsersBooks(string page, string limit);
    public Task<int> GetAllBooksWithUserIdPagesCountAsync(string limit);
}