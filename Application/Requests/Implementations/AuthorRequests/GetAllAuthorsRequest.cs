using Application.Requests.Abstractions;

namespace Application.Requests.Implementations.AuthorRequests;

public class GetAllAuthorsRequest: IRequest
{
    public GetAllAuthorsRequest(int page, int limit)
    {
        Page = page;
        Limit = limit;
    }

    public int Page { get; set; }
    public int Limit { get; set; }
}