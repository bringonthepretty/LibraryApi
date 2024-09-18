using System.Net;

namespace Domain.Exceptions;

public class LibraryApplicationException : Exception
{

    public List<string> ExceptionMessages { get; } = [];
    public HttpStatusCode HttpCode { get; }
    
    public LibraryApplicationException(HttpStatusCode httpCode)
    {
        HttpCode = httpCode;
    }

    public LibraryApplicationException(HttpStatusCode httpCode, string message)
        : base(message)
    {
        HttpCode = httpCode;
        ExceptionMessages.Add(message);   
    }
    
    public LibraryApplicationException(HttpStatusCode httpCode, List<string> messages)
    {
        HttpCode = httpCode;
        ExceptionMessages.AddRange(messages);
    } 
}