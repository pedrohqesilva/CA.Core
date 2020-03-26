using Domain.Services.Interfaces.Repositories;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repository
{
    public class WriteRepository<TEntity, TContext> : IWriteRepository<TEntity>
        where TEntity : class
        where TContext : BaseContext
    {
        private readonly TContext _contexto;

        public WriteRepository(TContext context)
        {
            _contexto = context;
        }

        public virtual TEntity Add(TEntity entity)
        {
            var result = _contexto.Set<TEntity>().Add(entity);
            return result.Entity;
        }

        public virtual Task AddRange(IList<TEntity> entity, CancellationToken cancellationToken)
        {
            var result = _contexto.Set<TEntity>().AddRangeAsync(entity, cancellationToken);
            return result;
        }

        public void Attach(TEntity entity)
        {
            if (_contexto.Entry(entity).State == EntityState.Detached)
            {
                _contexto.Set<TEntity>().Attach(entity);
            }
        }

        public virtual void Remove(TEntity entity)
        {
            _contexto.Set<TEntity>().Remove(entity);
        }

        public virtual void Update(TEntity entity)
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