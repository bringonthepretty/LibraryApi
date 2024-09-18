using Application.Dtos;
using Application.Services.Api;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Requests;
using Presentation.Views;

namespace Presentation.Controllers;

[Route("api/[controller]")]
public class AuthorsController(IAuthorService authorService) : ApiController
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return Ok(await authorService.GetByIdAsync(id));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllWithPageAndLimit(string page = "1", string limit = "10")
    {
        return Ok(await authorService.GetAllWithPageAndLimitAsync(page, limit));
    }

    [HttpPost]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateOrUpdateAuthorRequest model)
    {
        var dto = model.Adapt<AuthorDto>();
        return Ok(await authorService.CreateAsync(dto));
    }
    
    [HttpPut("{id:guid}")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> Update([FromBody] CreateOrUpdateAuthorRequest model, Guid id)
    {
        var dto = model.Adapt<AuthorDto>();
        dto.Id = id;
        return Ok(await authorService.UpdateAsync(dto));
    }
    
    [HttpDelete("{id:guid}")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return Ok(await authorService.DeleteAsync(id));
    }
    
    [HttpGet("pagescount")]
    public async Task<IActionResult> GetAllBooksPagesCount(string limit = "10")
    {
        return Ok(await authorService.GetAllAuthorsPagesCountAsync(limit));
    }
}