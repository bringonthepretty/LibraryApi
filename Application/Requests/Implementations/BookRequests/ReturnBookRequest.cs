using Application.Requests.Abstractions;

namespace Application.Requests.Implementations.BookRequests;

public class ReturnBookRequest: IRequest
{
    public ReturnBookRequest(Guid userId, Guid bookId)
    {
        UserId = userId;
        BookId = bookId;
    }

    public Guid UserId { get; set; }
    public Guid BookId { get; set; }
}