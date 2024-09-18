using Application.Dtos;

namespace Application.Services.Api;

public interface IAuthorService
{
    public Task<AuthorDto> CreateAsync(AuthorDto author);
    public Task<AuthorDto> GetByIdAsync(Guid id);
    public Task<List<AuthorDto>> GetAllWithPageAndLimitAsync(string page, string limit);
    public Task<AuthorDto> UpdateAsync(AuthorDto author);
    public Task<bool> DeleteAsync(Guid id);
    public Task<int> GetAllAuthorsCountAsync();
    public Task<int> GetAllAuthorsPagesCountAsync(string entriesOnPage);
}