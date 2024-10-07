namespace Application.Dtos.FilterMode;

public class ByUserId(Guid userId): IFilterMode
{
    public Guid UserId { get; } = userId;
}