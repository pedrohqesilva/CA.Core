using Domain.Specifications.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Services.Interfaces.Repositories
{
    public interface IReadRepository<TEntity, TContext>
    {
        IQueryable<TEntity> AsQuerable();

        IQueryable<TEntity> Where(ISpecification<TEntity> specification);

        Task<bool> All(ISpecification<TEntity> specification, CancellationToken cancellationToken);

        Task<bool> Any(ISpecification<TEntity> specification, CancellationToken cancellationToken);

        Task<int> Count(ISpecification<TEntity> specification, CancellationToken cancellationToken);

        Task<List<TEntity>> Search(ISpecification<TEntity> specification, CancellationToken cancellationToken);

        Task<List<TEntity>> Search(ISpecification<TEntity> specification, int pageNumber, int pageSize, CancellationToken cancellationToken);

        Task<List<TEntity>> ToList(CancellationToken cancellationToken);

        Task<TEntity> FirstOrDefault(ISpecification<TEntity> specification, CancellationToken cancellationToken);

        ValueTask<TEntity> Find(CancellationToken cancellationToken, params object[] keys);
    }
}