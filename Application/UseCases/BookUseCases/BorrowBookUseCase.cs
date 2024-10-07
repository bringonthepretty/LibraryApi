using System.Net;
using Application.DependencyInjectionExtensions;
using Application.Exceptions;
using Domain.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.BookUseCases;

[Service]
public class BorrowBookUseCase(IBookRepository bookRepository)
{
    public async Task<bool> InvokeAsync(Guid userId, Guid bookId)
    {
        var book = await bookRepository.GetByIdAsync(bookId);
        if (book is null)
        {
            throw new LibraryApplicationException(ExceptionCode.EntityDoesNotExists, "Book does not exists");
        }

        if (!book.Available)
        {
            throw new LibraryApplicationException(ExceptionCode.ImpossibleData, "Book already taken");
        }

        book.Available = false;
        book.BorrowedByUserId = userId;
        book.BorrowTime = DateTime.Now.AddDays(7);

        bookRepository.Update(book);
        await bookRepository.SaveChangesAsync();
        
        return true;
    }
}