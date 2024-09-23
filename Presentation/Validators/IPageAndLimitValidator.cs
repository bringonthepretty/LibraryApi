namespace Presentation.Validators;

public interface IPageAndLimitValidator
{
    public void ValidatePageAndLimit(string page, string limit, out int intPage, out int intLimit);
    public void ValidateLimit(string limit, out int intLimit);
}