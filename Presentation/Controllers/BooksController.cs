using Application.Dtos;
using Application.Dtos.FilterMode;
using Application.Requests.Implementations.AuthorRequests;
using Application.Requests.Implementations.BookRequests;
using Application.UseCases.BookUseCases;
using Application.UseCases.Boundaries;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Requests;
using Presentation.Validators;

namespace Presentation.Controllers;

[Route("api/[controller]")]
public class BooksController(Boundary boundary, IPageAndLimitValidator pageAndLimitValidator) : ApiController
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return Ok(await boundary.InvokeAsync(new GetBookByIdRequest(id)));
    }
    
    [HttpGet("byIsbn")]
    public async Task<IActionResult> GetByIsbn(string isbn)
    {
        return Ok(await boundary.InvokeAsync(new GetBookByIsbnRequest(isbn)));
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllWithPageAndLimit(string page = "1", string limit = "10")
    {
        pageAndLimitValidator.ValidatePageAndLimit(page, limit, out var intPage, out var intLimit);
        return Ok(await boundary.InvokeAsync(new GetAllBooksRequest
        (
            new BlankFilter(),
            intPage,
            intLimit
        )));
    }

    [HttpPost]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> Create(CreateOrUpdateBookWebRequest model)
    {
        var request = model.Adapt<CreateBookRequest>();
        return Ok(await boundary.InvokeAsync(request));
    }

    [HttpPut("{id:guid}")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> Update(CreateOrUpdateBookWebRequest model, Guid id)
    {
        var request = model.Adapt<UpdateAuthorRequest>();
        request.Id = id;
        return Ok(await boundary.InvokeAsync(request));
    }
    
    [HttpDelete("{id:guid}")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> DeleteBook(Guid id)
    {
        return Ok(await boundary.InvokeAsync(new DeleteBookRequest(id)));
    }
    
    [HttpGet("{bookId:guid}/borrow")]
    [Authorize(Policy = "Authenticated")]
    public async Task<IActionResult> BorrowBook(Guid userId, Guid bookId)
    {
        var result = await boundary.InvokeAsync(new BorrowBookRequest(userId, bookId));
        return Ok(result);
    }
    
    [HttpGet("{bookId:guid}/return")]
    [Authorize(Policy = "Authenticated")]
    public async Task<IActionResult> ReturnBook(Guid userId, Guid bookId)
    {
        var result = await boundary.InvokeAsync(new ReturnBookRequest(userId, bookId));
        return Ok(result);
    }

    [HttpGet("byName")]
    public async Task<IActionResult> GetAllByNameWithPageAndLimit(string name, string page = "1", string limit = "10")
    {
        pageAndLimitValidator.ValidatePageAndLimit(page, limit, out var intPage, out var intLimit);
        return Ok(await boundary.InvokeAsync(new GetAllBooksRequest
        (
            new ByName(name),
            intPage,
            intLimit
        )));
    }
    
    [HttpGet("byGenre")]
    public async Task<IActionResult> GetAllByGenreWithPageAndLimit(string genre, string page = "1", string limit = "10")
    {
        pageAndLimitValidator.ValidatePageAndLimit(page, limit, out var intPage, out var intLimit);
        return Ok(await boundary.InvokeAsync(new GetAllBooksRequest
        (
            new ByGenre(genre),
            intPage,
            intLimit
        )));
    }
    
    [HttpGet("byAuthor")]
    public async Task<IActionResult> GetAllByAuthorWithPageAndLimit(Guid authorId, string page = "1", string limit = "10")
    {
        pageAndLimitValidator.ValidatePageAndLimit(page, limit, out var intPage, out var intLimit);
        return Ok(await boundary.InvokeAsync(new GetAllBooksRequest
        (
            new ByAuthorId(authorId),
            intPage,
            intLimit
        )));
    }
    
    [HttpGet("pagescount")]
    public async Task<IActionResult> GetAllBooksPagesCount(string limit = "10")
    {
        pageAndLimitValidator.ValidateLimit(limit, out var intLimit);
        return Ok(await boundary.InvokeAsync(new GetAllBooksPageCountRequest
        (
            new BlankFilter(),
            intLimit
        )));
    }

    [HttpGet("pagescountbyname")]
    public async Task<IActionResult> GetAllBooksWithNamePagesCount(string name, string limit = "10")
    {
        pageAndLimitValidator.ValidateLimit(limit, out var intLimit);
        return Ok(await boundary.InvokeAsync(new GetAllBooksPageCountRequest
        (
            new ByName(name),
            intLimit
        )));
    }
    
    [HttpGet("pagescountbygenre")]
    public async Task<IActionResult> GetAllBooksWithGenrePagesCount(string genre, string limit = "10")
    {
        pageAndLimitValidator.ValidateLimit(limit, out var intLimit);
        return Ok(await boundary.InvokeAsync(new GetAllBooksPageCountRequest
        (
            new ByGenre(genre),
            intLimit
        )));
    }
    
    [HttpGet("pagescountbyauthor")]
    public async Task<IActionResult> GetAllBooksWithAuthorPagesCount(Guid id, string limit = "10")
    {
        pageAndLimitValidator.ValidateLimit(limit, out var intLimit);
        return Ok(await boundary.InvokeAsync(new GetAllBooksPageCountRequest
        (
            new ByAuthorId(id),
            intLimit
        )));
    }
}