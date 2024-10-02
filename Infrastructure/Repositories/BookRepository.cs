using Application.DependencyInjectionExtensions;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

[Service]
public class BookRepository(LibraryDbContext dbContext) : IBookRepository
{
    public async Task<Book> CreateAsync(Book book)
    {
        var result = dbContext.Books.Add(book);
        return result.Entity;
    }

    public async Task<Book?> GetByIdAsync(Guid id)
    {
        return await dbContext.Books.Include(book => book.Author).FirstOrDefaultAsync(book => book.Id == id);
    }

    public async Task<Book?> GetByIsbnAsync(string isbn)
    {
        return await dbContext.Books.Include(book => book.Author).FirstOrDefaultAsync(book => book.Isbn == isbn);
    }

    public async Task<Book?> GetByExactNameAsync(string name)
    {
        return await dbContext.Books.Include(book => book.Author).FirstOrDefaultAsync(book => book.Name == name);
    }

    public async Task<List<Book>> GetAllAsync()
    {
        return await dbContext.Books.Include(book => book.Author).ToListAsync();
    }

    public async Task<List<Book>> GetAllByAuthorIdAsync(Guid authorId)
    {
        return await dbContext.Books.Where(book => book.AuthorId == authorId).Include(book => book.Author).ToListAsync();
    }

    public async Task<List<Book>> GetAllByNamePartAsync(string namePart)
    {
        return await dbContext.Books.Where(book => book.Name.ToLower().Contains(namePart.ToLower())).Include(book => book.Author).ToListAsync();
    }

    public async Task<List<Book>> GetAllByGenreAsync(string genre)
    {
        return await dbContext.Books.Where(book => book.Genre.ToLower() == genre.ToLower()).Include(book => book.Author).ToListAsync();
    }

    public async Task<List<Book>> GetAllWithOffsetAndLimitAsync(int offset, int limit)
    {
        return await dbContext.Books.OrderBy(book => book.Id).Include(book => book.Author).Skip(offset).Take(limit).ToListAsync();
    }

    public async Task<List<Book>> GetAllByAuthorIdWithOffsetAndLimitAsync(Guid authorId, int offset, int limit)
    {
        return await dbContext.Books.OrderBy(book => book.Id).Where(book => book.AuthorId == authorId).Include(book => book.Author).Skip(offset).Take(limit).ToListAsync();
    }

    public async Task<List<Book>> GetAllByUserIdWithOffsetAndLimitAsync(Guid userId, int offset, int limit)
    {
        return await dbContext.Books.OrderBy(book => book.Id).Where(book => book.BorrowedByUserId == userId).Include(book => book.Author).Skip(offset).Take(limit).ToListAsync();
    }

    public async Task<List<Book>> GetAllByNamePartWithOffsetAndLimitAsync(string namePart, int offset, int limit)
    {
        return await dbContext.Books.OrderBy(book => book.Id).Where(book => book.Name.ToLower().Contains(namePart.ToLower())).Include(book => book.Author).Skip(offset).Take(limit).ToListAsync();
    }

    public async Task<List<Book>> GetAllByGenreWithOffsetAndLimitAsync(string genre, int offset, int limit)
    {
        return await dbContext.Books.OrderBy(book => book.Id).Where(book => book.Genre.ToLower() == genre.ToLower()).Include(book => book.Author).Skip(offset).Take(limit).ToListAsync();
    }

    public async Task<Book> UpdateAsync(Book book)
    {
        dbContext.Books.Update(book);
        return book;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        dbContext.Remove(new Book { Id = id });
        return true;
    }

    public async Task<int> DeleteByAuthorIdAsync(Guid authorId)
    {
        dbContext.Books.RemoveRange(dbContext.Books.Where(book => book.AuthorId == authorId));
        return 0;
    }

    public async Task<int> CountAllAsync()
    {
        return await dbContext.Books.CountAsync();
    }

    public async Task<int> CountAllWithNamePartAsync(string namePart)
    {
        return await dbContext.Books.Where(book => book.Name.Contains(namePart)).CountAsync();
    }

    public async Task<int> CountAllWithAuthorIdAsync(Guid authorId)
    {
        return await dbContext.Books.Where(book => book.AuthorId == authorId).CountAsync();
    }

    public async Task<int> CountAllWithUserIdAsync(Guid userId)
    {
        return await dbContext.Books.Where(book => book.BorrowedByUserId == userId).CountAsync();
    }

    public async Task<int> CountAllWithGenreAsync(string genre)
    {
        return await dbContext.Books.Where(book => book.Genre == genre).CountAsync();
    }
    
    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}