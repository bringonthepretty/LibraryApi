namespace Application.Dtos;

public record TokenDto(string AccessToken, string RefreshToken, Guid Id, string Role);