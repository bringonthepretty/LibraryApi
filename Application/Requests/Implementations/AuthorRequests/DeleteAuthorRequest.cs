using Application.Requests.Abstractions;

namespace Application.Requests.Implementations.AuthorRequests;

public class DeleteAuthorRequest: IRequest
{
    public DeleteAuthorRequest(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}