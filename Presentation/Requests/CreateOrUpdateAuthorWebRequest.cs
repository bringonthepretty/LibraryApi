namespace Presentation.Requests;

public class CreateOrUpdateAuthorWebRequest
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateOnly BirthDate { get; set; }
    public string Country { get; set; }
}