using Application.Requests.Abstractions;

namespace Application.Requests.Implementations.BookRequests;

public class GetBookByIsbnRequest: IRequest
{
    public GetBookByIsbnRequest(string isbn)
    {
        Isbn = isbn;
    }

    public string Isbn { get; set; }
}