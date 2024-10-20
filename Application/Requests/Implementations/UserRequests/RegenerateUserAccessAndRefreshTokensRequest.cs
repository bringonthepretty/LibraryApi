using Application.Requests.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Application.Requests.Implementations.UserRequests;

public class RegenerateUserAccessAndRefreshTokensRequest: IRequest
{
    public RegenerateUserAccessAndRefreshTokensRequest(string? oldRefreshToken, IResponseCookies responseCookies)
    {
        OldRefreshToken = oldRefreshToken;
        ResponseCookies = responseCookies;
    }

    public string? OldRefreshToken { get; set; }
    public IResponseCookies ResponseCookies { get; set; }
}