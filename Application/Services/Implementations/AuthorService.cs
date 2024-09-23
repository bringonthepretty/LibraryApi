using System.Net;
using Application.Dtos;
using Application.Services.Api;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Exceptions;
using Mapster;

namespace Application.Services.Implementations;

public class AuthorService(IAuthorRepository authorRepository, IBookService bookService) : IAuthorService
{
    public async Task<AuthorDto> CreateAsync(AuthorDto author)
    {
        var authorDbEntity = author.Adapt<Author>();
        return (await authorRepository.CreateAsync(authorDbEntity)).Adapt<AuthorDto>();
    }

    public async Task<AuthorDto> GetByIdAsync(Guid id)
    {
        var result = await authorRepository.GetByIdAsync(id);

        if (result is null)
        {
            throw new LibraryApplicationException(HttpStatusCode.NotFound, "There is no author with given id");
        }

        return result.Adapt<AuthorDto>();
    }

    public async Task<List<AuthorDto>> GetAllWithPageAndLimitAsync(int page, int limit)
    {
        var offset = (page - 1) * limit;

        return (await authorRepository.GetAllWithOffsetAndLimitAsync(offset, limit)).Select(author => author.Adapt<AuthorDto>()).ToList();
    }

    public async Task<AuthorDto> UpdateAsync(AuthorDto author)
    {
        var dbAuthor = author.Adapt<Author>();
        return (await authorRepository.UpdateAsync(dbAuthor)).Adapt<AuthorDto>();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        await bookService.DeleteByAuthorIdAsync(id);
        return await authorRepository.DeleteAsync(id);
    }

    public async Task<int> GetAllAuthorsCountAsync()
    {
        return await authorRepository.CountAsync();
    }

    public async Task<int> GetAllAuthorsPagesCountAsync(int entriesOnPage)
    {
        var count = await GetAllAuthorsCountAsync();
        return (count + entriesOnPage - 1) / entriesOnPage;
    }
}