using Application.Requests.Abstractions;

namespace Application.Requests.Implementations.AuthorRequests;

public class CreateAuthorRequest: IRequest
{
    public CreateAuthorRequest(string name, string surname, DateOnly birthDate, string country)
    {
        Name = name;
        Surname = surname;
        BirthDate = birthDate;
        Country = country;
    }

    public string Name { get; set; }
    public string Surname { get; set; }
    public DateOnly BirthDate { get; set; }
    public string Country { get; set; }
}