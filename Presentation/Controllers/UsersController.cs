using Application.Services.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Validators;

namespace Presentation.Controllers;

[Route("api/Home")]
public class UsersController(IUserService userService, IPageAndLimitValidator pageAndLimitValidator) : ApiController
{
    [HttpGet("books")]
    [Authorize(Policy = "Authenticated")]
    public async Task<IActionResult> GetUsersBooks(string page = "1", string limit = "10")
    {
        pageAndLimitValidator.ValidatePageAndLimit(page, limit, out var intPage, out var intLimit);
        return Ok(await userService.GetUsersBooks(intPage, intLimit));
    }
    
    [HttpGet("bookspagescount")]
    public async Task<IActionResult> GetAllBooksWithNamePagesCount(string limit = "10")
    {
        pageAndLimitValidator.ValidateLimit(limit, out var intLimit);
        return Ok(await userService.GetAllBooksWithUserIdPagesCountAsync(intLimit));
    }
}