using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure;

public sealed class LibraryDbContext(DbContextOptions options, IConfiguration configuration) : DbContext(options)
{
    public DbSet<User> Users { get; init; }
    public DbSet<Book> Books { get; init; }
    public DbSet<Author> Authors { get; init; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("Postgresql"));

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureUser(modelBuilder);

        ConfigureBook(modelBuilder);
        
        ConfigureAuthor(modelBuilder);
    }

    private void ConfigureUser(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasKey(user => user.Id);

        modelBuilder.Entity<User>()
            .Property(user => user.Username)
            .HasMaxLength(50)
            .IsRequired();
        
        modelBuilder.Entity<User>()
            .Property(user => user.Login)
            .HasMaxLength(30)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(user => user.Role)
            .HasMaxLength(30)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(user => user.PasswordKey)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(user => user.PasswordHash)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(user => user.RefreshToken)
            .IsRequired(false);
    }

    private void ConfigureBook(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
            .HasKey(book => book.Id);
        
        modelBuilder.Entity<Book>()
            .Property(book => book.Name)
            .HasMaxLength(100)
            .IsRequired();
        
        modelBuilder.Entity<Book>()
            .Property(book => book.Isbn)
            .HasMaxLength(100)
            .IsRequired();

        modelBuilder.Entity<Book>()
            .Property(book => book.Genre)
            .HasMaxLength(50)
            .IsRequired();

        modelBuilder.Entity<Book>()
            .Property(book => book.Description)
            .HasMaxLength(1000)
            .IsRequired();

        modelBuilder.Entity<Book>()
            .Property(book => book.Available)
            .IsRequired();

        modelBuilder.Entity<Book>()
            .Property(book => book.BorrowedByUserId)
            .IsRequired(false);

        modelBuilder.Entity<Book>()
            .Property(book => book.BorrowTime)
            .IsRequired(false);

        modelBuilder.Entity<Book>()
            .Property(book => book.Image)
            .IsRequired();

        modelBuilder.Entity<Book>()
            .HasOne<Author>(book => book.Author)
            .WithMany()
            .HasForeignKey(book => book.AuthorId);
    }

    private void ConfigureAuthor(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>()
            .HasKey(author => author.Id);

        modelBuilder.Entity<Author>()
            .Property(author => author.Name)
            .HasMaxLength(20)
            .IsRequired();
        
        modelBuilder.Entity<Author>()
            .Property(author => author.Surname)
            .HasMaxLength(20)
            .IsRequired();
        
        modelBuilder.Entity<Author>()
            .Property(author => author.BirthDate)
            .IsRequired();
        
        modelBuilder.Entity<Author>()
            .Property(author => author.Country)
            .HasMaxLength(20)
            .IsRequired();
    }
}