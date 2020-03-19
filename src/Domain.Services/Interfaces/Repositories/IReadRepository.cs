using Domain.Specifications.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Services.Interfaces.Repositories
{
    public interface IReadRepository<T>
    {
        IQueryable<T> AsQuerable();

        IQueryable<T> Where(ISpecification<T> specification);

        Task<bool> All(ISpecification<T> specification, CancellationToken cancellationToken);

        Task<bool> Any(ISpecification<T> specification, CancellationToken cancellationToken);

        Task<int> Count(ISpecification<T> specification, CancellationToken cancellationToken);

        Task<List<T>> Search(ISpecification<T> specification, CancellationToken cancellationToken);

        Task<List<T>> Search(ISpecification<T> specification, int pageNumber, int pageSize, CancellationToken cancellationToken);

        Task<T> FirstOrDefault(ISpecification<T> specification, CancellationToken cancellationToken);

        ValueTask<T> Find(CancellationToken cancellationToken, params object[] keys);
    }
}