namespace Domain.Services.Interfaces.Repositories
{
    public interface IReadWriteRepository<TEntity> : IReadRepository<TEntity>, IWriteRepository<TEntity>
    {
    }
}