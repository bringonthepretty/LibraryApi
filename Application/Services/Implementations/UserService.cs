using System.Net;
using Application.Dtos;
using Application.Services.Api;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Exceptions;
using Mapster;
using Microsoft.AspNetCore.Http;

namespace Application.Services.Implementations;

public class UserService(IUserRepository userRepository, IBookService bookService, IHttpContextAccessor contextAccessor) : BaseService, IUserService
{

    public async Task<UserDto> GetByIdAsync(Guid id)
    {
        var result = await userRepository.GetByIdAsync(id);

        if (result is null)
        {
            throw new LibraryApplicationException(HttpStatusCode.NotFound, "There is no user with given id");
        }

        return result.Adapt<UserDto>();
    }

    public async Task<UserDto> GetByLoginAsync(string login)
    {
        var result = await userRepository.GetByLoginAsync(login);

        if (result is null)
        {
            throw new LibraryApplicationException(HttpStatusCode.NotFound, "There is no user with given login");
        }

        return result.Adapt<UserDto>();
    }

    public async Task<List<BookDto>> GetUsersBooks(string page, string limit)
    {
        var currentUser = contextAccessor.HttpContext?.User;
        var idClaim = currentUser?.Claims.FirstOrDefault(claim => claim.Type == "Id");
        
        if (idClaim is null)
        {
            throw new LibraryApplicationException(HttpStatusCode.Unauthorized, "User is not authenticated");
        }

        return await bookService.GetAllByUserWithPageAndLimitAsync(Guid.Parse(idClaim.Value), page, limit);
    }

    public async Task<int> GetAllBooksWithUserIdPagesCountAsync(string limit)
    {
        var currentUser = contextAccessor.HttpContext?.User;
        var idClaim = currentUser?.Claims.FirstOrDefault(claim => claim.Type == "Id");
        
        if (idClaim is null)
        {
            throw new LibraryApplicationException(HttpStatusCode.Unauthorized, "User is not authenticated");
        }

        return await bookService.GetAllBooksWithUserIdPagesCountAsync(Guid.Parse(idClaim.Value), limit);
    }
}