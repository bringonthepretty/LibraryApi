using Application.Requests.Abstractions;

namespace Application.Requests.Implementations.AuthorRequests;

public class GetAuthorByIdRequest: IRequest
{
    public GetAuthorByIdRequest(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}