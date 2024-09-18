using Application.Services.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/Home")]
public class UsersController(IUserService userService) : ApiController
{
    [HttpGet("books")]
    [Authorize(Policy = "Authenticated")]
    public async Task<IActionResult> GetUsersBooks(string page = "1", string limit = "10")
    {
        return Ok(await userService.GetUsersBooks(page, limit));
    }
    
    [HttpGet("bookspagescount")]
    public async Task<IActionResult> GetAllBooksWithNamePagesCount(string limit = "10")
    {
        return Ok(await userService.GetAllBooksWithUserIdPagesCountAsync(limit));
    }
}