using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Services.Interfaces.Repositories
{
    public interface IWriteRepository<TEntity, TContext>
    {
        TEntity Add(TEntity entity);

        Task AddRange(IList<TEntity> entity, CancellationToken cancellationToken);

        void Attach(TEntity entity);

        void Remove(TEntity entity);

        void Update(TEntity entity);

        Task<int> SaveChanges(CancellationToken cancellationToken);
    }
}