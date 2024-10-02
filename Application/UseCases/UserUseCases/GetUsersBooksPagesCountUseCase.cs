using System.Net;
using Application.DependencyInjectionExtensions;
using Application.Exceptions;
using Application.UseCases.BookUseCases;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.UserUseCases;

[Service]
public class GetUsersBooksPagesCountUseCase(IHttpContextAccessor contextAccessor, GetAllBooksWithUserIdPagesCountUseCase getAllBooksWithUserIdPagesCountUseCase)
{
    public async Task<int> InvokeAsync(int limit)
    {
        var currentUser = contextAccessor.HttpContext?.User;
        var idClaim = currentUser?.Claims.FirstOrDefault(claim => claim.Type == "Id");
        
        if (idClaim is null)
        {
            throw new LibraryApplicationException(HttpStatusCode.Unauthorized, "User is not authenticated");
        }

        return await getAllBooksWithUserIdPagesCountUseCase.InvokeAsync(Guid.Parse(idClaim.Value), limit);
    }
}