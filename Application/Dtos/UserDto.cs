namespace Application.Dtos;

public class UserDto : EntityDto
{
    public string Username { get; set; }
    public string Login { get; set; }
    public string Role { get; set; }
    public byte[] PasswordKey { get; set; }
    public byte[] PasswordHash { get; set; }
    
    public string? RefreshToken { get; set; }

}