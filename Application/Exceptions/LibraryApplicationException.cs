using System.Net;

namespace Application.Exceptions;

public class LibraryApplicationException : Exception
{

    public List<string> ExceptionMessages { get; } = [];
    public ExceptionCode ExceptionCode { get; }
    
    public LibraryApplicationException(ExceptionCode exceptionCode)
    {
        ExceptionCode = exceptionCode;
    }
    
    public LibraryApplicationException(string message)
        : base(message)
    {
        ExceptionMessages.Add(message);   
    }

    public LibraryApplicationException(ExceptionCode exceptionCode, string message)
        : base(message)
    {
        ExceptionCode = exceptionCode;
        ExceptionMessages.Add(message);   
    }
    
    public LibraryApplicationException(ExceptionCode exceptionCode, List<string> messages)
    {
        ExceptionCode = exceptionCode;
        ExceptionMessages.AddRange(messages);
    } 
}