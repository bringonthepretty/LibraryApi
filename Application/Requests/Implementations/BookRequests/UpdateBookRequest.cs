using Application.Requests.Abstractions;

namespace Application.Requests.Implementations.BookRequests;

public class UpdateBookRequest: IRequest
{
    public UpdateBookRequest(Guid id, string isbn, string name, string genre, string description, string image, Guid authorId)
    {
        Id = id;
        Isbn = isbn;
        Name = name;
        Genre = genre;
        Description = description;
        Image = image;
        AuthorId = authorId;
    }

    public Guid Id { get; set; }
    public string Isbn { get; set; }
    public string Name { get; set; }
    public string Genre { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public Guid AuthorId { get; set; }
}