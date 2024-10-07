namespace Application.Dtos.FilterMode;

public class ByName(string name) : IFilterMode
{
    public string Name { get; } = name;
};