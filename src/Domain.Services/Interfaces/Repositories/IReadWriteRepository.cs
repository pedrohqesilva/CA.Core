namespace Domain.Services.Interfaces.Repositories
{
    public interface IReadWriteRepository<T> : IReadRepository<T>, IWriteRepository<T>
    {
    }
}