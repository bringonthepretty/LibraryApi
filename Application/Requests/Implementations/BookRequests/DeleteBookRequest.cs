using Application.Requests.Abstractions;

namespace Application.Requests.Implementations.BookRequests;

public class DeleteBookRequest: IRequest
{
    public DeleteBookRequest(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}