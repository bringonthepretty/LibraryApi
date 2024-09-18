namespace Application.Dtos;

public class AuthorDto : EntityDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateOnly BirthDate { get; set; }
    public string Country { get; set; }
}