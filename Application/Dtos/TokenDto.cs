namespace Application.Dtos;

public record TokenDto(string AccessToken, Guid Id, string Role);