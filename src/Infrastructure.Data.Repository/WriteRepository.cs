using Domain.Interfaces.Repositories.Bases;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repository.Bases
{
    public class WriteRepository<T> : IWriteRepository<T> where T : class
    {
        private readonly BaseContext _contexto;

        public WriteRepository(BaseContext context)
        {
            _contexto = context;
        }

        public virtual T Add(T entity)
        {
            var result = _contexto.Set<T>().Add(entity);
            return result.Entity;
        }

        public virtual Task AddRange(IList<T> entity, CancellationToken cancellationToken)
        {
            var result = _contexto.Set<T>().AddRangeAsync(entity, cancellationToken);
            return result;
        }

        public void Attach(T entity)
        {
            if (_contexto.Entry(entity).State == EntityState.Detached)
            {
                _contexto.Set<T>().Attach(entity);
            }
        }

        public virtual void Remove(T entity)
        {
            _contexto.Set<T>().Remove(entity);
        }

        public virtual void Update(T entity)
        {
            Attach(entity);
            _contexto.Entry(entity).State = EntityState.Modified;
        }

        public Task<int> SaveChanges(CancellationToken cancellationToken)
        {
            return _contexto.SaveChangesAsync(cancellationToken);
        }
    }
}