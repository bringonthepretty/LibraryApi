using Application.Dtos;
using Application.UseCases.BookUseCases;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Requests;
using Presentation.Validators;

namespace Presentation.Controllers;

[Route("api/[controller]")]
public class BooksController(IPageAndLimitValidator pageAndLimitValidator, BookUseCases bookUseCases) : ApiController
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return Ok(await bookUseCases.GetBookByIdUseCase.InvokeAsync(id));
    }
    
    [HttpGet("byIsbn")]
    public async Task<IActionResult> GetByIsbn(string isbn)
    {
        return Ok(await bookUseCases.GetBookByIsbnUseCase.InvokeAsync(isbn));
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllWithPageAndLimit(string page = "1", string limit = "10")
    {
        pageAndLimitValidator.ValidatePageAndLimit(page, limit, out var intPage, out var intLimit);
        return Ok(await bookUseCases.GetAllBooksWithPageAndLimitUseCase.InvokeAsync(intPage, intLimit));
    }

    [HttpPost]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> Create(CreateOrUpdateBookRequest model)
    {
        var dto = model.Adapt<BookDto>();
        return Ok(await bookUseCases.CreateBookUseCase.InvokeAsync(dto));
    }

    [HttpPut("{id:guid}")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> Update(CreateOrUpdateBookRequest model, Guid id)
    {
        var dto = model.Adapt<BookDto>();
        dto.Id = id;
        return Ok(await bookUseCases.UpdateBookUseCase.InvokeAsync(dto));
    }
    
    [HttpDelete("{id:guid}")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> DeleteBook(Guid id)
    {
        return Ok(await bookUseCases.DeleteBookUseCase.InvokeAsync(id));
    }
    
    [HttpGet("{id:guid}/borrow")]
    [Authorize(Policy = "Authenticated")]
    public async Task<IActionResult> BorrowBook(Guid id)
    {
        var result = await bookUseCases.BorrowBookUseCase.InvokeAsync(id);
        return Ok(result);
    }
    
    [HttpGet("{id:guid}/return")]
    [Authorize(Policy = "Authenticated")]
    public async Task<IActionResult> ReturnBook(Guid id)
    {
        var result = await bookUseCases.ReturnBookUseCase.InvokeAsync(id);
        return Ok(result);
    }

    [HttpGet("byName")]
    public async Task<IActionResult> GetAllByNameWithPageAndLimit(string name, string page = "1", string limit = "10")
    {
        pageAndLimitValidator.ValidatePageAndLimit(page, limit, out var intPage, out var intLimit);
        return Ok(await bookUseCases.GetAllBooksByNameWithPageAndLimitUseCase.InvokeAsync(name, intPage, intLimit));
    }
    
    [HttpGet("byGenre")]
    public async Task<IActionResult> GetAllByGenreWithPageAndLimit(string genre, string page = "1", string limit = "10")
    {
        pageAndLimitValidator.ValidatePageAndLimit(page, limit, out var intPage, out var intLimit);
        return Ok(await bookUseCases.GetAllBooksByGenreWithPageAndLimitUseCase.InvokeAsync(genre, intPage, intLimit));
    }
    
    [HttpGet("byAuthor")]
    public async Task<IActionResult> GetAllByAuthorWithPageAndLimit(Guid authorId, string page = "1", string limit = "10")
    {
        pageAndLimitValidator.ValidatePageAndLimit(page, limit, out var intPage, out var intLimit);
        return Ok(await bookUseCases.GetAllBooksByAuthorWithPageAndLimitUseCase.InvokeAsync(authorId, intPage, intLimit));
    }
    
    [HttpGet("pagescount")]
    public async Task<IActionResult> GetAllBooksPagesCount(string limit = "10")
    {
        pageAndLimitValidator.ValidateLimit(limit, out var intLimit);
        return Ok(await bookUseCases.GetAllBooksPagesCountUseCase.InvokeAsync(intLimit));
    }

    [HttpGet("pagescountbyname")]
    public async Task<IActionResult> GetAllBooksWithNamePagesCount(string name, string limit = "10")
    {
        pageAndLimitValidator.ValidateLimit(limit, out var intLimit);
        return Ok(await bookUseCases.GetAllBooksWithNamePagesCountUseCase.InvokeAsync(name, intLimit));
    }
    
    [HttpGet("pagescountbygenre")]
    public async Task<IActionResult> GetAllBooksWithGenrePagesCount(string genre, string limit = "10")
    {
        pageAndLimitValidator.ValidateLimit(limit, out var intLimit);
        return Ok(await bookUseCases.GetAllBooksWithGenrePagesCountUseCase.InvokeAsync(genre, intLimit));
    }
    
    [HttpGet("pagescountbyauthor")]
    public async Task<IActionResult> GetAllBooksWithAuthorPagesCount(Guid id, string limit = "10")
    {
        pageAndLimitValidator.ValidateLimit(limit, out var intLimit);
        return Ok(await bookUseCases.GetAllBooksWithAuthorIdPagesCountUseCase.InvokeAsync(id, intLimit));
    }
}