using Application.Dtos;
using Application.Requests.Implementations.UserRequests;
using Application.UseCases.Boundaries;
using Application.UseCases.UserUseCases;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Requests;
using Presentation.Views;

namespace Presentation.Controllers;

[Route("api/[controller]")]
public class AuthController(Boundary boundary) : ApiController
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterWebRequest registerWebRequest)
    {
        
        await boundary.InvokeAsync(registerWebRequest.Adapt<RegisterUserRequest>());
        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginWebRequest loginWebRequest)
    {
        var result = await boundary.InvokeAsync(new LoginUserRequest(loginWebRequest.Login, loginWebRequest.Password, Response.Cookies));
        return Ok(result);
    }

    [HttpGet("regenerateTokens")]
    public async Task<IActionResult> RegenerateTokens()
    {
        var result = await boundary.InvokeAsync(new RegenerateUserAccessAndRefreshTokensRequest(Request.Cookies["RefreshToken"], Response.Cookies));
        return Ok(result);
    }
}