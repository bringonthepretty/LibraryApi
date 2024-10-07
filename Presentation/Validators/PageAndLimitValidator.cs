using System.Net;
using Application.DependencyInjectionExtensions;
using Application.Exceptions;

namespace Presentation.Validators;

[Service]
public class PageAndLimitValidator: IPageAndLimitValidator
{
    public void ValidatePageAndLimit(string page, string limit, out int intPage, out int intLimit)
    {
        var exceptionMessagesList = new List<string>();
        
        ValidateLimit(limit, out intLimit, exceptionMessagesList);
        var pageParseResult = int.TryParse(page, out intPage);

        if (!pageParseResult)
        {
            exceptionMessagesList.Add("Page must be valid int");
            throw new LibraryApplicationException(ExceptionCode.ImpossibleData, exceptionMessagesList);
        }

        if (intPage < 1)
        {
            exceptionMessagesList.Add("Page cannot be less than one");
            throw new LibraryApplicationException(ExceptionCode.ImpossibleData, exceptionMessagesList);
        }
    }

    public void ValidateLimit(string limit, out int intLimit)
    {
        var exceptionMessagesList = new List<string>();
        
        var limitParseResult = int.TryParse(limit, out intLimit);
        
        if (!limitParseResult)
        {
            throw new LibraryApplicationException(ExceptionCode.ImpossibleData, exceptionMessagesList);
        }

        if (intLimit < 1)
        {
            throw new LibraryApplicationException(ExceptionCode.ImpossibleData, exceptionMessagesList);
        }
    }

    private void ValidateLimit(string limit, out int intLimit, List<string> exceptionMessagesList)
    {
        var limitParseResult = int.TryParse(limit, out intLimit);
        
        if (!limitParseResult)
        {
            exceptionMessagesList.Add("Limit must be valid int");
            return;
        }

        if (intLimit < 1)
        {
            exceptionMessagesList.Add("Limit cannot be less than one");
        }
    }
}