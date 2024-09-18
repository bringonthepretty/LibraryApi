using System.Net;
using Application.Dtos;
using Application.Services.Api;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Exceptions;
using FluentValidation;
using Mapster;

namespace Application.Services.Implementations;

public class AuthorService(IAuthorRepository authorRepository, IBookService bookService, IValidator<AuthorDto> authorValidator) : BaseService, IAuthorService
{
    public async Task<AuthorDto> CreateAsync(AuthorDto author)
    {
        await authorValidator.ValidateAsync(author, options =>
        {
            options
                .ThrowOnFailures()
                .IncludeRuleSets("NonIdProperties");
        });
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

    public async Task<List<AuthorDto>> GetAllWithPageAndLimitAsync(string page, string limit)
    {
        ValidatePageAndLimit(page, limit, out var intPage, out var intLimit);

        var offset = (intPage - 1) * intLimit;

        return (await authorRepository.GetAllWithOffsetAndLimitAsync(offset, intLimit)).Select(author => author.Adapt<AuthorDto>()).ToList();
    }

    public async Task<AuthorDto> UpdateAsync(AuthorDto author)
    {
        await authorValidator.ValidateAsync(author, options =>
        {
            options
                .ThrowOnFailures()
                .IncludeRuleSets("NonIdProperties", "Id");
        });
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

    public async Task<int> GetAllAuthorsPagesCountAsync(string entriesOnPage)
    {
        ValidateLimit(entriesOnPage, out var intEntriesOnPage);
        var count = await GetAllAuthorsCountAsync();
        return (count + intEntriesOnPage - 1) / intEntriesOnPage;
    }
}