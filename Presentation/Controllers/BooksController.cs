using Application.Dtos;
using Application.Services.Api;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Requests;
using Presentation.Validators;

namespace Presentation.Controllers;

[Route("api/[controller]")]
public class BooksController(IBookService bookService, IPageAndLimitValidator pageAndLimitValidator) : ApiController
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return Ok(await bookService.GetByIdAsync(id));
    }
    
    [HttpGet("byIsbn")]
    public async Task<IActionResult> GetByIsbn(string isbn)
    {
        return Ok(await bookService.GetByIsbnAsync(isbn));
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllWithPageAndLimit(string page = "1", string limit = "10")
    {
        pageAndLimitValidator.ValidatePageAndLimit(page, limit, out var intPage, out var intLimit);
        return Ok(await bookService.GetAllWithPageAndLimitAsync(intPage, intLimit));
    }

    [HttpPost]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> Create(CreateOrUpdateBookRequest model)
    {
        var dto = model.Adapt<BookDto>();

        return Ok(await bookService.CreateAsync(dto));
    }

    [HttpPut("{id:guid}")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> Update(CreateOrUpdateBookRequest model, Guid id)
    {
        var dto = model.Adapt<BookDto>();
        dto.Id = id;
        return Ok(await bookService.UpdateAsync(dto));
    }
    
    [HttpDelete("{id:guid}")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> DeleteBook(Guid id)
    {
        return Ok(await bookService.DeleteAsync(id));
    }
    
    [HttpGet("{id:guid}/borrow")]
    [Authorize(Policy = "Authenticated")]
    public async Task<IActionResult> BorrowBook(Guid id)
    {
        var result = await bookService.BorrowBookAsync(id);
        return Ok(result);
    }
    
    [HttpGet("{id:guid}/return")]
    [Authorize(Policy = "Authenticated")]
    public async Task<IActionResult> ReturnBook(Guid id)
    {
        var result = await bookService.ReturnBookAsync(id);
        return Ok(result);
    }

    [HttpGet("byName")]
    public async Task<IActionResult> GetAllByNameWithPageAndLimit(string name, string page = "1", string limit = "10")
    {
        pageAndLimitValidator.ValidatePageAndLimit(page, limit, out var intPage, out var intLimit);
        return Ok(await bookService.GetAllByNameWithPageAndLimitAsync(name, intPage, intLimit));
    }
    
    [HttpGet("byGenre")]
    public async Task<IActionResult> GetAllByGenreWithPageAndLimit(string genre, string page = "1", string limit = "10")
    {
        pageAndLimitValidator.ValidatePageAndLimit(page, limit, out var intPage, out var intLimit);
        return Ok(await bookService.GetAllByGenreWithPageAndLimitAsync(genre, intPage, intLimit));
    }
    
    [HttpGet("byAuthor")]
    public async Task<IActionResult> GetAllByAuthorWithPageAndLimit(Guid authorId, string page = "1", string limit = "10")
    {
        pageAndLimitValidator.ValidatePageAndLimit(page, limit, out var intPage, out var intLimit);
        return Ok(await bookService.GetAllByAuthorWithPageAndLimitAsync(authorId, intPage, intLimit));
    }
    
    [HttpGet("pagescount")]
    public async Task<IActionResult> GetAllBooksPagesCount(string limit = "10")
    {
        pageAndLimitValidator.ValidateLimit(limit, out var intLimit);
        return Ok(await bookService.GetAllBooksPagesCountAsync(intLimit));
    }

    [HttpGet("pagescountbyname")]
    public async Task<IActionResult> GetAllBooksWithNamePagesCount(string name, string limit = "10")
    {
        pageAndLimitValidator.ValidateLimit(limit, out var intLimit);
        return Ok(await bookService.GetAllBooksWithNamePagesCountAsync(name, intLimit));
    }
    
    [HttpGet("pagescountbygenre")]
    public async Task<IActionResult> GetAllBooksWithGenrePagesCount(string genre, string limit = "10")
    {
        pageAndLimitValidator.ValidateLimit(limit, out var intLimit);
        return Ok(await bookService.GetAllBooksWithGenrePagesCountAsync(genre, intLimit));
    }
    
    [HttpGet("pagescountbyauthor")]
    public async Task<IActionResult> GetAllBooksWithAuthorPagesCount(Guid id, string limit = "10")
    {
        pageAndLimitValidator.ValidateLimit(limit, out var intLimit);
        return Ok(await bookService.GetAllBooksWithAuthorIdPagesCountAsync(id, intLimit));
    }
}