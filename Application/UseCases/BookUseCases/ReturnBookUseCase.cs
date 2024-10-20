using System.Net;
using Application.DependencyInjectionExtensions;
using Application.Exceptions;
using Application.Requests.Implementations.BookRequests;
using Domain.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.BookUseCases;

[Service]
public class ReturnBookUseCase(IBookRepository bookRepository)
{
    public async Task<bool> InvokeAsync(ReturnBookRequest request)
    {
        var book = await bookRepository.GetByIdAsync(request.BookId);
        if (book is null)
        {
            throw new LibraryApplicationException(ExceptionCode.EntityDoesNotExists, "Book with provided id does not exists");
        }

        if (book.BorrowedByUserId != request.UserId)
        {
            throw new LibraryApplicationException(ExceptionCode.AuthenticationError, "User does not own this book");
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