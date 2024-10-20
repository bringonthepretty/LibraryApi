using Application.Requests.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Application.Requests.Implementations.UserRequests;

public class LoginUserRequest: IRequest
{
    public LoginUserRequest(string login, string password, IResponseCookies responseCookies)
    {
        Login = login;
        Password = password;
        ResponseCookies = responseCookies;
    }

    public string Login { get; set; }
    public string Password { get; set; }
    public IResponseCookies ResponseCookies { get; set; }
}