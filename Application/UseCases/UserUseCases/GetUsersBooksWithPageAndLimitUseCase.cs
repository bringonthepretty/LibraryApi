using System.Net;
using Application.DependencyInjectionExtensions;
using Application.Dtos;
using Application.Exceptions;
using Application.UseCases.BookUseCases;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.UserUseCases;

[Service]
public class GetUsersBooksWithPageAndLimitUseCase(IHttpContextAccessor contextAccessor, GetAllBooksByUserWithPageAndLimitUseCase getAllBooksByUserWithPageAndLimitUseCase)
{
    public async Task<List<BookDto>> InvokeAsync(int page, int limit)
    {
        var currentUser = contextAccessor.HttpContext?.User;
        var idClaim = currentUser?.Claims.FirstOrDefault(claim => claim.Type == "Id");
        
        if (idClaim is null)
        {
            throw new LibraryApplicationException(HttpStatusCode.Unauthorized, "User is not authenticated");
        }

        return await getAllBooksByUserWithPageAndLimitUseCase.InvokeAsync(Guid.Parse(idClaim.Value), page, limit);
    }
}