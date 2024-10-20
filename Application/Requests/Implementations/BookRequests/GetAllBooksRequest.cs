using Application.Dtos.FilterMode;
using Application.Requests.Abstractions;

namespace Application.Requests.Implementations.BookRequests;

public class GetAllBooksRequest: IRequest
{
    public GetAllBooksRequest(IFilterMode filterMode, int page, int limit)
    {
        FilterMode = filterMode;
        Page = page;
        Limit = limit;
    }

    public IFilterMode FilterMode { get; set; }
    public int Page { get; set; }
    public int Limit { get; set; }
}