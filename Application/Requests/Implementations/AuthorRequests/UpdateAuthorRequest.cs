using Application.Requests.Abstractions;

namespace Application.Requests.Implementations.AuthorRequests;

public class UpdateAuthorRequest: IRequest
{
    public UpdateAuthorRequest(Guid id, string name, string surname, string country, DateOnly birthDate)
    {
        Id = id;
        Name = name;
        Surname = surname;
        Country = country;
        BirthDate = birthDate;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateOnly BirthDate { get; set; }
    public string Country { get; set; }
}