using Application.Requests.Abstractions;

namespace Application.Requests.Implementations.BookRequests;

public class GetBookByIdRequest: IRequest
{
    public GetBookByIdRequest(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}