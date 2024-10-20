using Application.Requests.Abstractions;

namespace Application.Requests.Implementations.UserRequests;

public class RegisterUserRequest: IRequest
{
    public RegisterUserRequest(string username, string login, string password)
    {
        Username = username;
        Login = login;
        Password = password;
    }

    public string Username { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
}