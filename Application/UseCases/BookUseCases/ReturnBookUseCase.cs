using System.Net;
using Application.DependencyInjectionExtensions;
using Application.Exceptions;
using Domain.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.BookUseCases;

[Service]
public class ReturnBookUseCase(IBookRepository bookRepository)
{
    public async Task<bool> InvokeAsync(Guid userId, Guid bookId)
    {
        var book = await bookRepository.GetByIdAsync(bookId);
        if (book is null)
        {
            throw new LibraryApplicationException(ExceptionCode.EntityDoesNotExists, "Book with provided id does not exusts");
        }

        if (book.BorrowedByUserId != userId)
        {
            throw new LibraryApplicationException(ExceptionCode.SecurityError, "User does not own this book");
        }
        
        if (book.Available)
        {
            return false;
        }

        book.Available = true;
        book.BorrowedByUserId = null;
        book.BorrowTime = null;

        bookRepository.Update(book);
        await bookRepository.SaveChangesAsync();
        
        return true;
    }
}