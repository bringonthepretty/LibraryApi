using Application.Dtos;

namespace Application.Services.Api;

public interface IAuthService
{
    public Task<bool> Register(RegisterRequestDto registerRequest);
    public Task<TokenDto> Login(string login, string password);
    public Task<TokenDto> RegenerateAccessAndRefreshTokens(string? oldRefreshToken);
}