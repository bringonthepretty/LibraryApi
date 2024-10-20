using Application.Dtos;
using Application.Dtos.FilterMode;
using Application.Requests.Implementations.BookRequests;
using Application.UseCases.BookUseCases;
using Application.UseCases.Boundaries;
using Application.UseCases.UserUseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Validators;

namespace Presentation.Controllers;

[Route("api/Home")]
public class UsersController(Boundary boundary,  IPageAndLimitValidator pageAndLimitValidator) : ApiController
{
    [HttpGet("books")]
    [Authorize(Policy = "Authenticated")]
    public async Task<IActionResult> GetUsersBooks(Guid userId, string page = "1", string limit = "10")
    {
        pageAndLimitValidator.ValidatePageAndLimit(page, limit, out var intPage, out var intLimit);
        return Ok(await boundary.InvokeAsync(new GetAllBooksRequest
        (
            new ByUserId(userId),
            intPage,
            intLimit
        )));
    }
    
    [HttpGet("bookspagescount")]
    public async Task<IActionResult> GetAllBooksWithNamePagesCount(Guid userId, string limit = "10")
    {
        pageAndLimitValidator.ValidateLimit(limit, out var intLimit);
        return Ok(await boundary.InvokeAsync(new GetAllBooksPageCountRequest
        (
            new ByUserId(userId),
            intLimit
        )));
    }
}