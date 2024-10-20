using Application.Requests.Abstractions;

namespace Application.Requests.Implementations.BookRequests;

public class BorrowBookRequest: IRequest
{
    public BorrowBookRequest(Guid userId, Guid bookId)
    {
        UserId = userId;
        BookId = bookId;
    }

    public Guid UserId { get; set; }
    public Guid BookId { get; set; }
}