namespace Application.Dtos.FilterMode;

public class ByAuthorId(Guid authorId): IFilterMode
{
    public Guid AuthorId { get; } = authorId;
}