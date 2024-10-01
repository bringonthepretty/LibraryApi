using System.Net;
using Application.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.BookUseCases;

public class BorrowBookUseCase(IHttpContextAccessor contextAccessor, UpdateBookUseCase updateBookUseCase, GetBookByIdUseCase getBookByIdUseCase)
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

        if (!book.Available)
        {
            throw new LibraryApplicationException(HttpStatusCode.UnprocessableContent, "Book already taken");
        }

        book.Available = false;
        book.BorrowedByUserId = Guid.Parse(idClaim.Value);
        book.BorrowTime = DateTime.Now.AddDays(7);

        await updateBookUseCase.InvokeAsync(book);
        
        return true;
    }
}