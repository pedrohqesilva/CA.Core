namespace Domain.Interfaces.Repositories.Bases
{
    public interface IReadWriteRepository<T> : IReadRepository<T>, IWriteRepository<T>
    {
    }
}