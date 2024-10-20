using Application.Requests.Abstractions;

namespace Application.Requests.Implementations.AuthorRequests;

public class GetAllAuthorsPagesCountRequest: IRequest
{
    public GetAllAuthorsPagesCountRequest(int entriesOnPage)
    {
        EntriesOnPage = entriesOnPage;
    }

    public int EntriesOnPage { get; set; }
}