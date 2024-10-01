using System.Net;
using Application.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.BookUseCases;

public class ReturnBookUseCase(IHttpContextAccessor contextAccessor, UpdateBookUseCase updateBookUseCase, GetBookByIdUseCase getBookByIdUseCase)
{
    public async Task<bool> InvokeAsync(Guid bookId)
    {
        var currentUser = contextAccessor.HttpContext?.User;
        var idClaim = currentUser?.Claims.FirstOrDefault(claim => claim.Type == "Id");
        
        if (idClaim is null)
        {
            throw new LibraryApplicationException(HttpStatusCode.Unauthorized, "User is not authenticated");
        }
        
        var book = await getBookByIdUseCase.InvokeAsync(bookId);

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

        await updateBookUseCase.InvokeAsync(book);

        return true;
    }
}