namespace Presentation.Requests;

public class CreateOrUpdateBookRequest
{
    public string Isbn { get; set; }
    public string Name { get; set; }
    public string Genre { get; set; }
    public string Description { get; set; }
    /// <summary>
    /// Base64 binary representation
    /// </summary>
    public string Image { get; set; }
    public Guid AuthorId { get; set; }
}