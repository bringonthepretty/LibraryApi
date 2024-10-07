namespace Application.Dtos.FilterMode;

public class ByGenre(string genre) : IFilterMode
{
    public string Genre { get; } = genre;
};