using Application.Dtos;
using Application.UseCases.UserUseCases;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Requests;
using Presentation.Views;

namespace Presentation.Controllers;

[Route("api/[controller]")]
public class AuthController(UserUseCases userUseCases) : ApiController
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
    {
        
        var result = await userUseCases.RegisterUserUseCase.InvokeAsync(registerRequest.Adapt<RegisterRequestDto>());
        if (result)
        {
            return Ok();
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var result = await userUseCases.LoginUserUseCase.InvokeAsync(loginRequest.Login, loginRequest.Password);
        Response.Cookies.Append("RefreshToken", result.RefreshToken, new CookieOptions(){HttpOnly = true, Secure = true});
        return Ok(new LoginResponseView
        {
            Id = result.Id,
            Token = result.AccessToken,
            Role = result.Role
        });
    }

    [HttpGet("regenerateTokens")]
    public async Task<IActionResult> RegenerateTokens()
    {
        var result = await userUseCases.RegenerateUserAccessAndRefreshTokensUseCase.InvokeAsync(Request.Cookies["RefreshToken"]);
        Response.Cookies.Append("RefreshToken", result.RefreshToken, new CookieOptions(){HttpOnly = true, Secure = true});
        return Ok(new LoginResponseView
        {
            Id = result.Id,
            Token = result.AccessToken,
            Role = result.Role
        });
    }
}