using System.Net;
using Application.Exceptions;

namespace Presentation.Validators;

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
            throw new LibraryApplicationException(HttpStatusCode.UnprocessableContent, exceptionMessagesList);
        }

        if (intPage < 1)
        {
            exceptionMessagesList.Add("Page cannot be less than one");
            throw new LibraryApplicationException(HttpStatusCode.UnprocessableContent, exceptionMessagesList);
        }
    }

    public void ValidateLimit(string limit, out int intLimit)
    {
        var exceptionMessagesList = new List<string>();
        
        var limitParseResult = int.TryParse(limit, out intLimit);
        
        if (!limitParseResult)
        {
            throw new LibraryApplicationException(HttpStatusCode.UnprocessableContent, exceptionMessagesList);
        }

        if (intLimit < 1)
        {
            throw new LibraryApplicationException(HttpStatusCode.UnprocessableContent, exceptionMessagesList);
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