using Application.Dtos.FilterMode;
using Application.Requests.Abstractions;

namespace Application.Requests.Implementations.BookRequests;

public class GetAllBooksPageCountRequest: IRequest
{
    public GetAllBooksPageCountRequest(IFilterMode filterMode, int limit)
    {
        FilterMode = filterMode;
        Limit = limit;
    }
    public IFilterMode FilterMode { get; set; }
    public int Limit { get; set; }
}