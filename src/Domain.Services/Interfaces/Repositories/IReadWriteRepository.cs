namespace Domain.Services.Interfaces.Repositories
{
    public interface IReadWriteRepository<TEntity, TContext> : IReadRepository<TEntity, TContext>, IWriteRepository<TEntity, TContext>
    {
    }
}