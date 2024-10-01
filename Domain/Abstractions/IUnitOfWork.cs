namespace Domain.Abstractions;

public interface IUnitOfWork
{
    public Task SaveChangesAsync();
}