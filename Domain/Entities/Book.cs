namespace Domain.Entities;

public class Book : Entity
{
    public string Isbn { get; set; }
    public string Name { get; set; }
    public string Genre { get; set; }
    public string Description { get; set; }
    public bool Available { get; set; }
    public Guid? BorrowedByUserId { get; set; }
    public DateTime? BorrowTime { get; set; }
    public string Image { get; set; }
    public Guid AuthorId { get; set; }
    public Author Author { get; set; }
}