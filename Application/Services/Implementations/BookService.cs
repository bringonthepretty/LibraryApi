using System.Net;
using Application.Dtos;
using Application.Exceptions;
using Application.Services.Api;
using Domain.Abstractions;
using Domain.Entities;
using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Http;

namespace Application.Services.Implementations;

public class BookService(IBookRepository bookRepository, IHttpContextAccessor contextAccessor): IBookService
{
    public async Task<BookDto> CreateAsync(BookDto book)
    {
        book.Available = true;
        var bookDbEntity = book.Adapt<Book>();
        return (await bookRepository.CreateAsync(bookDbEntity)).Adapt<BookDto>();
    }

    public async Task<BookDto> GetByIdAsync(Guid id)
    {
        var result = await bookRepository.GetByIdAsync(id);

        if (result is null)
        {
            throw new LibraryApplicationException(HttpStatusCode.NotFound, "There is no book with given id");
        }

        return result.Adapt<BookDto>();
    }
    
    public async Task<BookDto> GetByIsbnAsync(string isbn)
    {
        var result = await bookRepository.GetByIsbnAsync(isbn);

        if (result is null)
        {
            throw new LibraryApplicationException(HttpStatusCode.NotFound, "There is no book with given isbn");
        }

        return result.Adapt<BookDto>();
    }

    public async Task<List<BookDto>> GetAllWithPageAndLimitAsync(int page, int limit)
    {
        var offset = (page - 1) * limit;

        return (await bookRepository.GetAllWithOffsetAndLimitAsync(offset, limit)).Select(book => book.Adapt<BookDto>()).ToList();
    }

    public async Task<List<BookDto>> GetAllByNameWithPageAndLimitAsync(string name, int page, int limit)
    {

        var offset = (page - 1) * limit;

        return (await bookRepository.GetAllByNamePartWithOffsetAndLimitAsync(name, offset, limit)).Select(book => book.Adapt<BookDto>()).ToList();;
    }

    public async Task<List<BookDto>> GetAllByGenreWithPageAndLimitAsync(string genre, int page, int limit)
    {
        var offset = (page - 1) * limit;

        return (await bookRepository.GetAllByGenreWithOffsetAndLimitAsync(genre, offset, limit)).Select(book => book.Adapt<BookDto>()).ToList();;
    }

    public async Task<List<BookDto>> GetAllByAuthorWithPageAndLimitAsync(Guid authorId, int page, int limit)
    {
        var offset = (page - 1) * limit;

        return (await bookRepository.GetAllByAuthorIdWithOffsetAndLimitAsync(authorId, offset, limit)).Select(book => book.Adapt<BookDto>()).ToList();;
    }

    public async Task<List<BookDto>> GetAllByUserWithPageAndLimitAsync(Guid userId, int page, int limit)
    {
        var offset = (page - 1) * limit;
        
        return (await bookRepository.GetAllByUserIdWithOffsetAndLimitAsync(userId, offset, limit)).Select(book => book.Adapt<BookDto>()).ToList();;
    }

    public async Task<BookDto> UpdateAsync(BookDto book)
    {
        var bookDbEntity = book.Adapt<Book>();
        return (await bookRepository.UpdateAsync(bookDbEntity)).Adapt<BookDto>();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await bookRepository.DeleteAsync(id);
    }

    public async Task<int> DeleteByAuthorIdAsync(Guid authorId)
    {
        return await bookRepository.DeleteByAuthorIdAsync(authorId);
    }
    
    
    public async Task<bool> BorrowBookAsync(Guid bookId)
    {
        var currentUser = contextAccessor.HttpContext?.User;
        var idClaim = currentUser?.Claims.FirstOrDefault(claim => claim.Type == "Id");
        
        if (idClaim is null)
        {
            throw new LibraryApplicationException(HttpStatusCode.Unauthorized, "User is not authenticated");
        }

        var book = await GetByIdAsync(bookId);

        if (!book.Available)
        {
            throw new LibraryApplicationException(HttpStatusCode.UnprocessableContent, "Book already taken");
        }

        book.Available = false;
        book.BorrowedByUserId = Guid.Parse(idClaim.Value);
        book.BorrowTime = DateTime.Now.AddDays(7);

        await UpdateAsync(book);
        
        return true;
    }

    public async Task<bool> ReturnBookAsync(Guid bookId)
    {
        var currentUser = contextAccessor.HttpContext?.User;
        var idClaim = currentUser?.Claims.FirstOrDefault(claim => claim.Type == "Id");
        
        if (idClaim is null)
        {
            throw new LibraryApplicationException(HttpStatusCode.Unauthorized, "User is not authenticated");
        }
        
        var book = await GetByIdAsync(bookId);

        if (book.BorrowedByUserId != Guid.Parse(idClaim.Value))
        {
            throw new LibraryApplicationException(HttpStatusCode.Forbidden, "User does not own this book");
        }
        
        if (book.Available)
        {
            return false;
        }

        book.Available = true;
        book.BorrowedByUserId = null;
        book.BorrowTime = null;

        await UpdateAsync(book);

        return true;
    }

    public async Task<int> GetAllBooksCountAsync()
    {
        return await bookRepository.CountAllAsync();
    }

    public async Task<int> GetAllBooksPagesCountAsync(int entriesOnPage)
    {
        var count = await GetAllBooksCountAsync();
        return (count + entriesOnPage - 1) / entriesOnPage;
    }

    public async Task<int> GetAllBooksWithNameCountAsync(string name)
    {
        return await bookRepository.CountAllWithNamePartAsync(name);
    }

    public async Task<int> GetAllBooksWithNamePagesCountAsync(string name, int entriesOnPage)
    {
        var count = await bookRepository.CountAllWithNamePartAsync(name);
        return (count + entriesOnPage - 1) / entriesOnPage;
    }

    public async Task<int> GetAllBooksWithGenreCountAsync(string genre)
    {
        return await bookRepository.CountAllWithGenreAsync(genre);
    }

    public async Task<int> GetAllBooksWithGenrePagesCountAsync(string genre, int entriesOnPage)
    {
        var count = await bookRepository.CountAllWithGenreAsync(genre);
        return (count + entriesOnPage - 1) / entriesOnPage;
    }

    public async Task<int> GetAllBooksWithAuthorIdCountAsync(Guid authorId)
    {
        return await bookRepository.CountAllWithAuthorIdAsync(authorId);
    }

    public async Task<int> GetAllBooksWithAuthorIdPagesCountAsync(Guid authorId, int entriesOnPage)
    {
        var count = await bookRepository.CountAllWithAuthorIdAsync(authorId);
        return (count + entriesOnPage - 1) / entriesOnPage;
    }

    public async Task<int> GetAllBooksWithUserIdCountAsync(Guid userId)
    {
        return await bookRepository.CountAllWithUserIdAsync(userId);
    }

    public async Task<int> GetAllBooksWithUserIdPagesCountAsync(Guid userId, int entriesOnPage)
    {
        var count = await bookRepository.CountAllWithUserIdAsync(userId);
        return (count + entriesOnPage - 1) / entriesOnPage;
    }
}