using Application.Requests.Implementations.AuthorRequests;
using Application.UseCases.Boundaries;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Requests;
using Presentation.Validators;

namespace Presentation.Controllers;

[Route("api/[controller]")]
public class AuthorsController(Boundary boundary, IPageAndLimitValidator pageAndLimitValidator) : ApiController
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return Ok(await boundary.InvokeAsync(new GetAuthorByIdRequest(id)));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllWithPageAndLimit(string page = "1", string limit = "10")
    {
        pageAndLimitValidator.ValidatePageAndLimit(page, limit, out var intPage, out var intLimit);
        return Ok(await boundary.InvokeAsync(new GetAllAuthorsRequest(intPage, intLimit)));
    }

    [HttpPost]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateOrUpdateAuthorWebRequest model)
    {
        var request = model.Adapt<CreateAuthorRequest>();
        return Ok(await boundary.InvokeAsync(request));
    }
    
    [HttpPut("{id:guid}")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> Update([FromBody] CreateOrUpdateAuthorWebRequest model, Guid id)
    {
        var request = model.Adapt<UpdateAuthorRequest>();
        request.Id = id;
        return Ok(await boundary.InvokeAsync(request));
    }
    
    [HttpDelete("{id:guid}")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return Ok(await boundary.InvokeAsync(new DeleteAuthorRequest(id)));
    }
    
    [HttpGet("pagescount")]
    public async Task<IActionResult> GetAllBooksPagesCount(string limit = "10")
    {
        pageAndLimitValidator.ValidateLimit(limit, out var intLimit);
        return Ok(await boundary.InvokeAsync(new GetAllAuthorsPagesCountRequest(intLimit)));
    }
}