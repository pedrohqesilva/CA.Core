using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories.Bases
{
    public interface IWriteRepository<T>
    {
        T Add(T entity);

        Task AddRange(IList<T> entity, CancellationToken cancellationToken);

        void Attach(T entity);

        void Remove(T entity);

        void Update(T entity);

        Task<int> SaveChanges(CancellationToken cancellationToken);
    }
}